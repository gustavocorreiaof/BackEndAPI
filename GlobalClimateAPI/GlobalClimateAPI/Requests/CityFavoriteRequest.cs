using GlobalClimateAPI.Requests.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GlobalClimateAPI.Requests
{
    public class CityFavoriteRequest : FavoriteRequestBase
    {
        [Required]
        [JsonPropertyName("CityName")]
        [SwaggerSchema("CityName to create/delete Favorite City.")]
        public required string CityName { get; set; }

        public override string Name => CityName;
    }
}
