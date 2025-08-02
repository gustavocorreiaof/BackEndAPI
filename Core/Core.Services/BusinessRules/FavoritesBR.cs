using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Infrastructure.Data.Interfaces;
using Core.Services.BusinessRules.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Core.Services.BusinessRules
{
    public class FavoritesBR : IFavoritesBR
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IDynamoDbContext _dbContext;

        public FavoritesBR(IConfiguration config, IDynamoDbContext dbContext, HttpClient httpClient)
        {
            _config = config;
            _dbContext = dbContext;
            _httpClient = httpClient;
        }

        public async Task CreateFavoriteCity(FavoriteDTO favoriteDTO)
        {
            await VerifyIfActionIsValidToCities(favoriteDTO.Name, favoriteDTO.UserId, ActionType.Insert);

            FavoriteCity favoriteCity = new FavoriteCity()
            {
                UserId = favoriteDTO.UserId,
                CityName = favoriteDTO.Name
            };

            _ = _dbContext.InsertFavoriteCity(favoriteCity);
        }

        public async Task CreateFavoriteCountry(FavoriteDTO favoriteDTO)
        {
            await VerifyIfActionIsValidToCountry(favoriteDTO.Name, favoriteDTO.UserId, ActionType.Insert);

            FavoriteCountry favoriteCountry = new FavoriteCountry()
            {
                UserId = favoriteDTO.UserId,
                CountryName = favoriteDTO.Name
            };

            _ = _dbContext.InsertFavoriteCountry(favoriteCountry);
        }

        public async Task DeleteFavoriteCity(FavoriteDTO favoriteDTO)
        {
            FavoriteCity favoriteCity = await VerifyIfActionIsValidToCities(favoriteDTO.Name, favoriteDTO.UserId, ActionType.Delete);

            _ = _dbContext.DeleteFavoriteCity(favoriteCity.Id);
        }

        public async Task DeleteFavoriteCountry(FavoriteDTO favoriteDTO)
        {
            FavoriteCountry favoriteCountry = await VerifyIfActionIsValidToCountry(favoriteDTO.Name, favoriteDTO.UserId, ActionType.Delete);

            _ = _dbContext.DeleteFavoriteCountry(favoriteCountry.Id);
        }

        public Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId)
        {
            return _dbContext.GetAllFavoriteCityByUserId(userId);
        }

        public Task<List<FavoriteCountry>> GetAllFavoriteCountryByUserId(string userId)
        {
            return _dbContext.GetAllFavoriteCountryByUserId(userId);
        }

        private async Task<FavoriteCity> VerifyIfActionIsValidToCities(string cityName, string userId, ActionType actionType)
        {
            var apiKey = _config["OpenWeather:ApiKey"];
            var url = string.Format(_config["Urls:OpenWeatherMap"]!, cityName, apiKey);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new ApiException(ApiMsgs.INF001);

            _ = await _dbContext.GetUserById(userId) ?? throw new ApiException(ApiMsgs.EXC001);

            FavoriteCity favoriteCity = await _dbContext.GetFavoriteCityByCityNameAndUserId(cityName, userId);

            if (favoriteCity == null && actionType == ActionType.Delete)
                throw new ApiException(ApiMsgs.EXC003);
            else if (favoriteCity != null && actionType == ActionType.Insert)
                throw new ApiException(ApiMsgs.EXC004);

            return favoriteCity;
        }

        private async Task<FavoriteCountry> VerifyIfActionIsValidToCountry(string countryName, string userId, ActionType actionType)
        {
            var baseUrl = _config["Urls:RestCountries"];
            var url = $"{baseUrl}{countryName}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new ApiException(string.Format(ApiMsgs.INF002, countryName));

            _ = await _dbContext.GetUserById(userId) ?? throw new ApiException(ApiMsgs.EXC001);

            FavoriteCountry favoriteCountry = await _dbContext.GetFavoriteCountryByCountryNameAndUserId(countryName, userId);

            if (favoriteCountry == null && actionType == ActionType.Delete)
                throw new ApiException(ApiMsgs.EXC005);
            else if (favoriteCountry != null && actionType == ActionType.Insert)
                throw new ApiException(ApiMsgs.EXC006);

            return favoriteCountry;
        }
    }
}
