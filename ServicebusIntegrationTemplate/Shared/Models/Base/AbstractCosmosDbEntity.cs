using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optimera.INT.Order.Shared.Model.Base
{
    public class AbstractCosmosDbEntity
    {
        [JsonProperty("id")]
        public virtual string Id { get; set; }
        [JsonProperty("eTag")]
        [JsonIgnore]
        public string ETag { get; set; }
    }
}
