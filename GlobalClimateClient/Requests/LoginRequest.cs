using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace GlobalClimateAPI.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestsMsgs), ErrorMessageResourceName = "INF001")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
