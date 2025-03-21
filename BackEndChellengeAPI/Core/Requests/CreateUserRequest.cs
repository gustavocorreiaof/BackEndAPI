using Core.Util.Attributes;
using Core.Util.Msgs;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR001")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR002")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR003")]
        [PasswordValidation(ErrorMessage = "A senha deve conter pelo menos 6 caracteres, incluindo letras maiúsculas e minúsculas, números e um caractere especial.")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        [CpfValidation(ErrorMessage = "O CPF informado é inválido.")]
        public string CPF { get; set; }
    }
}