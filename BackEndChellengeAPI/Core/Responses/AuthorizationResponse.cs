using System.Text.Json.Serialization;

namespace Core.Responses
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
