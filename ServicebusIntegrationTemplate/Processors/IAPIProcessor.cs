using ServiceBusIntegrationTemplate.Shared.Models;
using System.Threading.Tasks;

namespace ServiceBusIntegrationTemplate.Processors
{
    public interface IAPIProcessor
    {
        Task<string> Post(ProductModel entity);
    }
}