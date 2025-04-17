using Core.API.Requests.Attributes;
using Core.Common.Enums;
using Core.Common.Util.Msgs;
using System.ComponentModel.DataAnnotations;

namespace Core.API.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR001")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR002")]
        public string Email { get; set; }

        [PasswordValidation]
        public string Password { get; set; }

        [CpfCnpjValidation]
        public string TaxNumber { get; set; }

        public UserType UserType { get; set; }
    }
}