using GlobalClimateAPI.Responses.Base;
using GlobalClimateAPI.Responses.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses
{
    public class GetWeatherResponse:BaseResponse
    {
        [SwaggerSchema("Contains the Weather's infos.")]
        public WeatherSummary WeatherInfos { get; set; }
    }
}
