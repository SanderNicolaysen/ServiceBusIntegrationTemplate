using Communicate.Http;
using Communicate.Http.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusIntegrationTemplate.Handlers;
using ServiceBusIntegrationTemplate.Handlers.Interface;
using ServiceBusIntegrationTemplate.Processors;
using ServiceBusIntegrationTemplate.Shared.Configurations;

namespace Optimera.INT.Order.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<ISbMessageHandler, SbMessageHandler>();
            services.AddTransient<ITemplateEventHandler, TemplateEventHandler>();
        }

        public static void AddProcessor(this IServiceCollection services)
        {
            services.AddTransient<IAPIProcessor, APIProcessor>();
        }

        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            TemplateConfig templateConfig = configuration.GetSection(TemplateConfig.TEMPLATE).Get<TemplateConfig>();
            services.Configure<TemplateConfig>(configuration.GetSection(TemplateConfig.TEMPLATE));

            HttpRestClient<DefaultHttpClientConfiguration<string>> apiClient = HttpRestClientFactory.Create<string>(templateConfig.BaseUrl, 3);
            apiClient.AddDefaultHeader(HttpRequestHeaders.ContentType, ContentTypes.ApplicationJson);
            apiClient.AddDefaultHeader(HttpRequestHeaders.Accept, ContentTypes.ApplicationJson);
            apiClient.AddDefaultHeader("Ocp-Apim-Subscription-Key", templateConfig.SubscriptionKey);

            apiClient.AddDefaultHeader("x-api-key", templateConfig.ApiKey);

            services.AddSingleton(apiClient);
        }
    }
}
