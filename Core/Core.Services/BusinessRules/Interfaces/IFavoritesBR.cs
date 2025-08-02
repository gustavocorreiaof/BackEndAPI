using Core.Domain.DTOs;
using Core.Domain.Entities;

namespace Core.Services.BusinessRules.Interfaces
{
    public interface IFavoritesBR
    {
        Task CreateFavoriteCity(FavoriteDTO favoriteDTO);
        Task CreateFavoriteCountry(FavoriteDTO favoriteDTO);
        Task DeleteFavoriteCity(FavoriteDTO favoriteDTO);
        Task DeleteFavoriteCountry(FavoriteDTO favoriteDTO);
        Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId);
        Task<List<FavoriteCountry>> GetAllFavoriteCountryByUserId(string userId);
    }
}
