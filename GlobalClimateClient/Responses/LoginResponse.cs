using GlobalClimateAPI.Responses.Base;

namespace GlobalClimateAPI.Responses
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}
