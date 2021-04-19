using Optimera.INT.Order.Shared.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusIntegrationTemplate.Shared.Models
{
    public class ProductModel : AbstractEntityMetaData
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
    }
}
