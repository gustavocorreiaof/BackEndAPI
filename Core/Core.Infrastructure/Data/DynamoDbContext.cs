using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Core.Domain.Entities;
using Core.Infrastructure.Data.Interfaces;

namespace Core.Infrastructure.Data
{
    public class DynamoDbContext : IDynamoDbContext
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly DynamoDBContext _context;

        public DynamoDbContext(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
            _context = new DynamoDBContext(_dynamoDbClient);
        }

        public async Task<User> GetUserById(string Id)
        {
            return await _context.LoadAsync<User>(Id);
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Name", ScanOperator.Equal, name)
            };

            var search = _context.ScanAsync<User>(conditions);
            var results = await search.GetNextSetAsync();
            return results.FirstOrDefault();
        }


        public async Task<List<FavoriteCity>> GetAllFavoriteCityByUserId(string userId)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("UserId", ScanOperator.Equal, userId)
            };

            var result = await _context.ScanAsync<FavoriteCity>(conditions).GetNextSetAsync();
            return result;
        }

        public async Task<List<FavoriteCountry>> GetAllFavoriteCountryByUserId(string userId)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("UserId", ScanOperator.Equal, userId)
            };

            var result = await _context.ScanAsync<FavoriteCountry>(conditions).GetNextSetAsync();
            return result;
        }

        public async Task<FavoriteCity> GetFavoriteCityByCityNameAndUserId(string cityName, string userId)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("CityName", ScanOperator.Equal, cityName),
                new ScanCondition("UserId", ScanOperator.Equal, userId)
            };

            var results = await _context.ScanAsync<FavoriteCity>(conditions).GetNextSetAsync();
            return results.FirstOrDefault()!;
        }

        public async Task<FavoriteCountry> GetFavoriteCountryByCountryNameAndUserId(string cityName, string userId)
        {
            var conditions = new List<ScanCondition>
                {
                    new ScanCondition("CountryName", ScanOperator.Equal, cityName),
                    new ScanCondition("UserId", ScanOperator.Equal, userId)
                };

            var results = await _context.ScanAsync<FavoriteCountry>(conditions).GetNextSetAsync();
            return results.FirstOrDefault()!;
        }



        public async Task<FavoriteCity> InsertFavoriteCity(FavoriteCity favoriteCity)
        {
            await _context.SaveAsync(favoriteCity);
            return favoriteCity;
        }

        public async Task<FavoriteCountry> InsertFavoriteCountry(FavoriteCountry favoriteCountry)
        {
            await _context.SaveAsync(favoriteCountry);
            return favoriteCountry;
        }


        public async Task DeleteFavoriteCity(string id)
        {
            await _context.DeleteAsync<FavoriteCity>(id);
        }

        public async Task DeleteFavoriteCountry(string id)
        {
            await _context.DeleteAsync<FavoriteCountry>(id);
        }
    }
}
