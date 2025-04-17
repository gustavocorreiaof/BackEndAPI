using System.Text.Json.Serialization;

namespace Core.Infrastructure.Json
{
    public class AuthorizationJson
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
