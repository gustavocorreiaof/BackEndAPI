using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Util.Attributes;
public class PasswordValidationAttribute : ValidationAttribute
{
    public PasswordValidationAttribute() : base("The password must contain at least 6 characters, including uppercase and lowercase letters, numbers, and a special character.") { }

    public override bool IsValid(object value)
    {
        if (value == null)
            return false;

        var password = value.ToString();

        var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!/%*?&])[A-Za-z\d@$!%*?&]{6,}$";

        return Regex.IsMatch(password, passwordRegex);
    }
}

