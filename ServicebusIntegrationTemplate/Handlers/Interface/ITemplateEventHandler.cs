using ServiceBusIntegrationTemplate.Shared.Models;
using System.Threading.Tasks;

namespace ServiceBusIntegrationTemplate.Handlers.Interface
{
    public interface ITemplateEventHandler
    {
        Task Handle(ProductModel productModel, int deliveryCount);
    }
}