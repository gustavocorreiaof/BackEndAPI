using GlobalClimateAPI.Responses.Base;
using GlobalClimateAPI.Responses.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses
{
    public class GetCountryResponse: BaseResponse
    {
        [SwaggerSchema("Contains the Country's infos.")]
        public CountryInfo CountryInfos { get; set; }
    }
}
