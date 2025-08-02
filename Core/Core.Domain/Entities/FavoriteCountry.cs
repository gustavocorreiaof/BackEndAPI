using Amazon.DynamoDBv2.DataModel;
using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    [DynamoDBTable("FavoriteCountry")]
    public class FavoriteCountry : BaseEntity
    {
        [DynamoDBProperty]
        public string CountryName { get; set; }

        [DynamoDBProperty]
        public string UserId { get; set; }

        [DynamoDBProperty]
        public DateTime FavoritedAt { get; set; } = DateTime.UtcNow;
    }
}
