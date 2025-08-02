using Core.Domain.Entities;
using GlobalClimateAPI.Responses.Base;

namespace GlobalClimateAPI.Responses
{
    public class GetFavoriteCitiesResponse : BaseResponse
    {
        public List<FavoriteCity> Cities { get; set; }
    }
}
