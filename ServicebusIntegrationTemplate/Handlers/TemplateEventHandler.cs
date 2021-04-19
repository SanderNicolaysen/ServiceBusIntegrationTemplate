using Communicate.Http;
using Communicate.Utilities.Archeo;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceBusIntegrationTemplate.Handlers.Interface;
using ServiceBusIntegrationTemplate.Processors;
using ServiceBusIntegrationTemplate.Shared.Models;
using ServiceBusIntegrationTemplate.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusIntegrationTemplate.Handlers
{
    public class TemplateEventHandler : ITemplateEventHandler
    {
        private readonly ILogger<TemplateEventHandler> _logger;
        private readonly IArcheoLogger _archeoLogger;
        private readonly IAPIProcessor _apiProcessor;
        private readonly SbEventValidator _sbEventValidator;

        public TemplateEventHandler(ILogger<TemplateEventHandler> logger, IArcheoLogger archeoLogger, SbEventValidator sbEventValidator, IAPIProcessor apiProcessor)
        {
            _logger = logger;
            _archeoLogger = archeoLogger;
            _apiProcessor = apiProcessor;
            _sbEventValidator = sbEventValidator;
        }

        public async Task Handle(ProductModel productModel, int deliveryCount)
        {
            if (productModel == null)
                throw new ArgumentException();

            ValidationResult results = _sbEventValidator.Validate(productModel);
            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    _logger.LogInformation(failure.ErrorMessage);
                    _archeoLogger.Log(JsonConvert.SerializeObject(productModel, Formatting.Indented), failure.ErrorMessage, fileName: "file.json", status: ArcheoDefaultStatuses.Success);
                    return;
                }
            }

            string response = "";
            try
            {
                response = await _apiProcessor.Post(productModel);
            }
            catch (HttpRestClientResponseException e)
            {
                string errorMessage = $"Post request failed. productId: {productModel.ProductId}";
                _logger.LogError(errorMessage);
                throw;
            }

            string resultMessage = $"App finished processing request. productId: {productModel.ProductId}";
        }
    }
}
