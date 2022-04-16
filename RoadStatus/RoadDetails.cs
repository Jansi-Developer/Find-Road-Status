using System;
using System.Text.Json.Serialization;

namespace RoadStatusAPI
{
    public class RoadDetails
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("displayName")]
        public string Name { get; set; }

        [JsonPropertyName("statusSeverityDescription")]
        public string StatusDescription { get; set; }

        [JsonPropertyName("statusSeverity")]
        public string Status { get; set; }

    }
}
