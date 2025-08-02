using Core.Domain.Entities;
using GlobalClimateAPI.Responses.Base;

namespace GlobalClimateAPI.Responses
{
    public class GetFavoriteCountriesResponse : BaseResponse
    {
        public List<FavoriteCountry> Countries { get; set; }
    }
}
