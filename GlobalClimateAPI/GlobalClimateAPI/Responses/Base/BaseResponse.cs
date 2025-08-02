using Swashbuckle.AspNetCore.Annotations;

namespace GlobalClimateAPI.Responses.Base
{
    public class BaseResponse
    {
        [SwaggerSchema("Indicate in the response whether the request was successful or not.")]
        public bool Success { get; set; }

        [SwaggerSchema("Informative message to indicate the reason for the error or a success message")]
        public string Message { get; set; }
    }
}
