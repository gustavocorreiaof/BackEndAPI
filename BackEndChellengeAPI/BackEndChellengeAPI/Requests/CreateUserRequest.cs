using BackEndChellengeAPI.Requests.Attributes;
using Core.Domain.Enums;
using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests
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