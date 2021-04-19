using Communicate.ErrorHandling.Rest.Exceptions;
using Communicate.Http;
using Communicate.Messaging.Interfaces;
using Communicate.Utilities.Archeo;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceBusIntegrationTemplate.Handlers.Interface;
using ServiceBusIntegrationTemplate.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusIntegrationTemplate.Handlers
{
    public class SbMessageHandler : ISbMessageHandler
    {
        private readonly ILogger<SbMessageHandler> _logger;

        public SbMessageHandler(ILogger<SbMessageHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleSbMessage<T>(Func<T, int, Task> specificHandler, int deliveryCount, Message sbMsg, ILogger log, MessageReceiver messageReceiver, string lockToken)
        {
            T msg = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(sbMsg.Body));

            //Check if message is delivered for retry
            if (deliveryCount > 1)
            {
                //Check if retry is not intentional
                if (!sbMsg.UserProperties.ContainsKey("IntentionalRetry"))
                {
                    //await messageReceiver.DeadLetterAsync(lockToken, "Deadlettered because retry was not intentional.");
                    //log.LogError("Message was deadlettered because it was recieved for non-intentional retry.");
                    //return;
                }
            }

            try
            {
                await specificHandler(msg, deliveryCount);
            }
            catch (HttpRestClientResponseException e)
            {
                await messageReceiver.DeadLetterAsync(lockToken, e.Message);
                log.LogCritical(e, e.Message);
                return;
            }
            catch (TransientException te) //Throw to retry message
            {
                await messageReceiver.AbandonAsync(lockToken, new Dictionary<string, object>() { { "IntentionalRetry", true } });
                log.LogError(te, "Message was abandoned because of intentional retry");
                return;
            }
            catch (NonTransientException nte) //Deadletter message before rethrowing to stop retry
            {
                await messageReceiver.DeadLetterAsync(lockToken, nte.Message);
                log.LogError(nte, "Message dead lettered because of handled non-transient exeption");
                return;
            }
            catch (Exception e)
            {
                string errorMessage = $"Unhandled error occured while processing request";

                await messageReceiver.DeadLetterAsync(lockToken, e.Message);
                log.LogError(e, "Message dead lettered because of non-handled non-transient exeption");
                return;
            }
            finally
            {

            }

            try
            {
                await messageReceiver.CompleteAsync(lockToken);
            }
            catch (Exception e)
            {
                log.LogError(e, "Could not complete message on SB. Duplicates are possible. Should be handled by retry logic.");
            }
        }
    }
}
