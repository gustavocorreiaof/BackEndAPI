using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Util.Attributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        // Mensagem padrão para o erro
        public PasswordValidationAttribute() : base("A senha deve conter pelo menos 6 caracteres, incluindo letras maiúsculas e minúsculas, números e um caractere especial.")
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var password = value.ToString();

            // Expressão regular para validar a senha
            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";

            // Verifica se a senha corresponde ao padrão
            return Regex.IsMatch(password, passwordRegex);
        }
    }
}
