using System.Text.Json.Serialization;

namespace Core.Common.Json
{
    public class Data
    {
        [JsonPropertyName("authorization")]
        public bool Authorization { get; set; }
    }
}
