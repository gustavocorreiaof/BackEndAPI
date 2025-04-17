using System.Text.Json.Serialization;

namespace Core.Common.Responses
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
