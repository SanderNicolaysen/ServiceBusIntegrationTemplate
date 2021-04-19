using Communicate.Http;
using Communicate.Utilities.Archeo;
using Microsoft.Extensions.Logging;
using ServiceBusIntegrationTemplate.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusIntegrationTemplate.Processors
{
    public class APIProcessor : IAPIProcessor
    {
        private readonly ILogger<APIProcessor> _logger;
        private readonly HttpRestClient<DefaultHttpClientConfiguration<string>> _apiClient;

        public APIProcessor(ILogger<APIProcessor> logger, HttpRestClient<DefaultHttpClientConfiguration<string>> apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<string> Post(ProductModel entity)
        {
            KeyValuePair<string, string>[] requestHeaders = { new KeyValuePair<string, string>("correlationId", "") };

            string response = await _apiClient.Post("request", entity, requestHeaders);

            return response;
        }
    }
}
