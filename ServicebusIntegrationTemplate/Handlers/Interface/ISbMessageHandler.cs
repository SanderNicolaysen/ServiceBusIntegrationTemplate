using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ServiceBusIntegrationTemplate.Handlers.Interface
{
    public interface ISbMessageHandler
    {
        Task HandleSbMessage<T>(Func<T, int, Task> specificHandler, int deliveryCount, Message sbMsg, ILogger log, MessageReceiver messageReceiver, string lockToken);
    }
}