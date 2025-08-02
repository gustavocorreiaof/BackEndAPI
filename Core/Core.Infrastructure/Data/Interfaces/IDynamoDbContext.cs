using Core.Domain.Entities;

namespace Core.Infrastructure.Data.Interfaces
{
    public interface IDynamoDbContext
    {
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserById(string Id);
        
        Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId);
        Task<FavoriteCity> GetFavoriteCityByCityNameAndUserId(string cityName, string userId);
        
        Task<List<FavoriteCountry>> GetAllFavoriteCountryByUserId(string userId);
        Task<FavoriteCountry> GetFavoriteCountryByCountryNameAndUserId(string cityName, string userId);


        Task<FavoriteCity> InsertFavoriteCity(FavoriteCity favoriteCity);
        Task<FavoriteCountry> InsertFavoriteCountry(FavoriteCountry favoriteCountry);
        
        Task DeleteFavoriteCity(string id);
        Task DeleteFavoriteCountry(string id);
    }
}
