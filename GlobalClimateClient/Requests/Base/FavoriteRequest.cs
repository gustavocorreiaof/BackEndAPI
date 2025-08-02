using System.ComponentModel.DataAnnotations;

namespace GlobalClimateAPI.Requests.Base
{
    public abstract class FavoriteRequestBase
    {
        [Required]
        public required string UserId { get; set; }

        public abstract string Name { get; }
    }
}
