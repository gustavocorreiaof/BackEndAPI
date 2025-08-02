using Core.Domain.Msgs;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GlobalClimateAPI.Requests
{
    public class LoginRequest
    {
        [SwaggerSchema("User's DynamoDB login name.", Nullable = false)]
        [Required(ErrorMessageResourceType = typeof(RequestsMsgs), ErrorMessageResourceName = "INF001")]
        public required string UserName { get; set; }

        [SwaggerSchema("User's DynamoDB password.", Nullable = false)]
        [Required]
        public required string Password { get; set; }
    }
}
