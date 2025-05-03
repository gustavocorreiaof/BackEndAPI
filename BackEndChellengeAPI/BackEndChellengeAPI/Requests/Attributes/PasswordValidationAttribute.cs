using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BackEndChellengeAPI.Requests.Attributes;
public class PasswordValidationAttribute : ValidationAttribute
{
    public PasswordValidationAttribute() : base(RequestMsg.ERR009) { }

    public override bool IsValid(object value)
    {
        if (value == null)
            return false;

        var password = value.ToString();

        var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$";

        return Regex.IsMatch(password, passwordRegex);
    }
}

