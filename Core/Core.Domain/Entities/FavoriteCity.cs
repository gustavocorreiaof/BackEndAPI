using Amazon.DynamoDBv2.DataModel;
using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    [DynamoDBTable("FavoriteCity")]
    public class FavoriteCity : BaseEntity
    {
        [DynamoDBProperty]
        public string CityName { get; set; }

        [DynamoDBProperty]
        public string UserId { get; set; }

        [DynamoDBProperty]
        public DateTime FavoritedAt { get; set; } = DateTime.UtcNow;
    }
}
