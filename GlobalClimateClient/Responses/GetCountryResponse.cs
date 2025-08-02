using GlobalClimateAPI.Responses.Base;
using GlobalClimateClient.Responses.Models;

namespace GlobalClimateAPI.Responses
{
    public class GetCountryResponse: BaseResponse
    {
        public CountryInfo CountryInfos { get; set; }
    }
}
