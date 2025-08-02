using Core.Domain.Entities;
using GlobalClimateAPI.Responses.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses
{
    public class GetFavoriteCitiesResponse : BaseResponse
    {
        [SwaggerSchema("User's favorite cities.")]
        public List<FavoriteCity> Cities { get; set; }
    }
}
