using GlobalClimateAPI.Responses.Base;
using GlobalClimateClient.Responses.Models;

namespace GlobalClimateAPI.Responses
{
    public class GetWeatherResponse:BaseResponse
    {
        public WeatherSummary WeatherInfos { get; set; }
    }
}
