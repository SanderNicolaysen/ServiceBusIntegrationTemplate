using Communicate.Utilities.Archeo;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optimera.INT.Order.Shared.Extensions;
using ServiceBusIntegrationTemplate;
using ServiceBusIntegrationTemplate.Validators;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(ServiceBusIntegrationTemplate.Startup))]
namespace ServiceBusIntegrationTemplate
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _configuration = GetConfiguration(builder);
            ConfigureServices(builder.Services);
        }

        private IConfiguration GetConfiguration(IFunctionsHostBuilder builder)
        {
            var tempProvider = builder.Services.BuildServiceProvider();
            var rootConfig = tempProvider.GetRequiredService<IConfiguration>();

            return rootConfig;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHandlers();
            services.AddProcessor();
            services.AddHttpClient(_configuration);

            // Validators
            services.AddScoped<SbEventValidator>();
        }
    }
}
