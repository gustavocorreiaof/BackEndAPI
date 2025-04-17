using System.Text.Json.Serialization;

namespace Core.Infrastructure.Json
{
    public class Data
    {
        [JsonPropertyName("authorization")]
        public bool Authorization { get; set; }
    }
}
