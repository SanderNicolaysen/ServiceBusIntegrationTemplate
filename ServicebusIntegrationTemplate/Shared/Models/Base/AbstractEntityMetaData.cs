using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optimera.INT.Order.Shared.Model.Base
{
    public abstract class AbstractEntityMetaData : AbstractCosmosDbEntity
    {
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("createdAtUtc")]
        public DateTime? CreatedAtUtc { get; set; }

        [JsonProperty("updatedAtUtc")]
        public DateTime? UpdatedAtUtc { get; set; }

        [JsonIgnore]
        [JsonProperty("cosmosEntityName")]
        public string CosmosEntityName { get; set; }

        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }
    }
}
