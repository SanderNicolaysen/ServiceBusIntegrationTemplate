using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus;
using ServiceBusIntegrationTemplate.Shared.Models;
using ServiceBusIntegrationTemplate.Handlers.Interface;

namespace ServiceBusIntegrationTemplate.Functions
{
    public class EntryPoint
    {
        private readonly ISbMessageHandler _sbMessageHandler;
        private readonly ITemplateEventHandler _templateEventHandler;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(ISbMessageHandler sbMessageHandler, ITemplateEventHandler templateEventHandler, ILogger<EntryPoint> logger)
        {
            _sbMessageHandler = sbMessageHandler;
            _templateEventHandler = templateEventHandler;
            _logger = logger;
        }

        [FunctionName("EntryPoint")]
        public async Task Run([ServiceBusTrigger("myTopic", "mySubscription", Connection = "ServiceBus:ConnectionString")] Message sbMsg,
        int deliveryCount, MessageReceiver messageReceiver, string lockToken)
        {
            await _sbMessageHandler.HandleSbMessage<ProductModel>(_templateEventHandler.Handle, deliveryCount, sbMsg, _logger, messageReceiver, lockToken);
        }
    }
}