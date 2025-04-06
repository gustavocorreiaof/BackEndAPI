using System.Text.Json.Serialization;

namespace Core.Responses
{
    public class Data
    {
        [JsonPropertyName("authorization")]
        public bool authorization { get; set; }   
    }
}
