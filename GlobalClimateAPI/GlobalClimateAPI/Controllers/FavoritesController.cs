using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Services.BusinessRules.Interfaces;
using GlobalClimateAPI.Requests;
using GlobalClimateAPI.Responses;
using GlobalClimateAPI.Responses.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesBR _IFavoritesBR;

        public FavoritesController(IFavoritesBR iFavoritesBR)
        {
            _IFavoritesBR = iFavoritesBR;
        }

        #region CITIES CONTEXT

        [HttpGet("GetFavoriteCitiesByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFavoriteCitiesResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Returns the user's favorite cities.", Description = "Recive Id's User to search his favorite cities.")]
        public async Task<IActionResult> GetFavoriteCitiesByUserId([FromQuery, SwaggerParameter("The userId to search his favorite cities.", Required = true)] string userId)
        {
            try
            {
                List<FavoriteCity> favoriteCities = await _IFavoritesBR.GetAllFavoriteCityByUserId(userId);

                return Ok(new GetFavoriteCitiesResponse() { Success = true, Cities = favoriteCities });
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

        [HttpPost("AddFavoriteCityByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Create new FavoriteCity to User.", Description = "Recive UserId and CityName to add the city in user's favorite cities.")]
        public async Task<IActionResult> AddFavoriteCityByUserId([FromBody] CityFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CityName);

                await _IFavoritesBR.CreateFavoriteCity(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF005 });
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

        [HttpDelete("DeleteFavoriteCityByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Delete city from user's favorite cities.", Description = "Recive UserId and CityName to remove city from user's favorite cities.")]
        public async Task<IActionResult> DeleteFavoriteCityByUserId([FromBody] CityFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CityName);

                await _IFavoritesBR.DeleteFavoriteCity(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF006 });
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

        #endregion

        #region Countries CONTEXT

        [HttpGet("GetFavoriteCountriesByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFavoriteCountriesResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Returns the user's favorite countries.", Description = "Recive Id's User to search his favorite countries.")]
        public async Task<IActionResult> GetFavoriteCountriesByUserId([FromQuery, SwaggerParameter("The userId to search his favorite countries.", Required = true)] string userId)
        {
            try
            {
                List<FavoriteCountry> favoriteCountries = await _IFavoritesBR.GetAllFavoriteCountryByUserId(userId);

                return Ok(new GetFavoriteCountriesResponse() { Success = true, Countries = favoriteCountries });
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

        [HttpPost("AddFavoriteCountryByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Create new FavoriteCountry to User.", Description = "Recive UserId and CountryName to add the Country in user's favorite countries.")]
        public async Task<IActionResult> AddFavoriteCountryByUserId([FromBody] CountryFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CountryName);

                await _IFavoritesBR.CreateFavoriteCountry(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF007 });
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

        [HttpDelete("DeleteFavoriteCountryByUserId")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Delete Country from user's favorite countries.", Description = "Recive UserId and CountryName to remove Country from user's favorite countries.")]
        public async Task<IActionResult> DeleteFavoriteCountryByUserId([FromBody] CountryFavoriteRequest request)
        {
            try
            {
                FavoriteDTO favoriteDTO = new FavoriteDTO(request.UserId, request.CountryName);

                await _IFavoritesBR.DeleteFavoriteCountry(favoriteDTO);

                return Ok(new BaseResponse() { Success = true, Message = ApiMsgs.INF008 });
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

        #endregion
    }
}
