using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using GlobalClimateAPI.Responses;
using GlobalClimateAPI.Responses.Base;
using GlobalClimateAPI.Responses.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace GlobalClimateAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CountryController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCountryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Returns information about a country.", Description = "Searches for information in the RestCountries API based on the name provided by the user.")]
        public async Task<IActionResult> GetCountry([FromQuery, SwaggerParameter("The name of the country to retrieve.", Required = true)] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ApiException(ApiMsgs.INF003);

                var baseUrl = _config["Urls:RestCountries"];
                var url = $"{baseUrl}{name}";

                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(new BaseResponse() { Success = false, Message = string.Format(ApiMsgs.INF002, name) });

                var json = await response.Content.ReadAsStringAsync();
                var document = JsonDocument.Parse(json);
                var countryJson = document.RootElement[0];

                return Ok(new GetCountryResponse
                {
                    Success = true,
                    CountryInfos = GetCountryInfo(countryJson)
                });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ApiMsgs.INF004,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        private CountryInfo GetCountryInfo(JsonElement countryJson)
        {
            var country = new CountryInfo
            {
                Name = countryJson.GetProperty("name").GetProperty("common").GetString(),
                Capital = countryJson.TryGetProperty("capital", out var capitalEl) && capitalEl.ValueKind == JsonValueKind.Array
                        ? capitalEl[0].GetString()
                        : null,
                Region = countryJson.GetProperty("region").GetString(),
                Language = countryJson.GetProperty("languages").EnumerateObject().FirstOrDefault().Value.GetString(),
                Population = countryJson.GetProperty("population").GetInt64()
            };

            return country;
        }
    }
}
