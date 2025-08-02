using GlobalClimateAPI.Requests.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GlobalClimateAPI.Requests
{
    public class CountryFavoriteRequest : FavoriteRequestBase
    {
        [Required]
        [JsonPropertyName("CountryName")]
        public required string CountryName { get; set; }

        public override string Name => CountryName;
    }
}
