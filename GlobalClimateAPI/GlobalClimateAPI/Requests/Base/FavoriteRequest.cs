using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GlobalClimateAPI.Requests.Base
{
    public abstract class FavoriteRequestBase
    {
        [Required]
        [SwaggerSchema("User's Id to create/delete a new favorite.")]
        public required string UserId { get; set; }

        public abstract string Name { get; }
    }
}
