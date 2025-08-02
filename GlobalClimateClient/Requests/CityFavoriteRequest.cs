using GlobalClimateAPI.Requests.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GlobalClimateAPI.Requests
{
    public class CityFavoriteRequest : FavoriteRequestBase
    {
        [Required]
        [JsonPropertyName("CityName")]
        public required string CityName { get; set; }

        public override string Name => CityName;
    }
}
