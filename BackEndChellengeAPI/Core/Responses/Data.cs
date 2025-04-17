using System.Text.Json.Serialization;

namespace Core.Common.Responses
{
    public class Data
    {
        [JsonPropertyName("authorization")]
        public bool authorization { get; set; }
    }
}
