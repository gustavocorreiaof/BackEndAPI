using GlobalClimateAPI.Responses.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses
{
    public class LoginResponse : BaseResponse
    {
        [SwaggerSchema("Contains the JWT token.")]
        public string Token { get; set; }
    }
}
