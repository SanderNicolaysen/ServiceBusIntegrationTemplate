using FluentValidation;
using ServiceBusIntegrationTemplate.Shared.Models;

namespace ServiceBusIntegrationTemplate.Validators
{
    public class SbEventValidator : AbstractValidator<ProductModel>
    {
        public SbEventValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ProductId).NotNull();
        }
    }
}