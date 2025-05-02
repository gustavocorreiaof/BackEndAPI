using Core.Domain.Enums;
using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests.Attributes;

public class CpfCnpjValidationAttribute : ValidationAttribute
{
    private string _relatedProperty = "UserType";
    public CpfCnpjValidationAttribute() : base(RequestMsg.ERR006) { }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult(ErrorMessage);

        var relatedPropertyInfo = validationContext.ObjectType.GetProperty(_relatedProperty);

        var document = new string(value.ToString().Where(char.IsDigit).ToArray());

        bool isValid = document.Length switch
        {
            11 => ValidateCPF(document),
            14 => ValidateCNPJ(document),
            _ => false
        };

        if (!isValid)
        {
            return new ValidationResult(ErrorMessage);
        }

        if (validationContext.ObjectInstance is CreateUserRequest)
        {
            if (document.Length == 11)
                relatedPropertyInfo.SetValue(validationContext.ObjectInstance, UserType.CPF);
            else if (document.Length == 14)
                relatedPropertyInfo.SetValue(validationContext.ObjectInstance, UserType.CNPJ);
        }

        return ValidationResult.Success;
    }

    private bool ValidateCPF(string cpf)
    {
        if (cpf.All(c => c == cpf[0])) return false;

        var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        int sum1 = 0;
        for (int i = 0; i < 9; i++) sum1 += cpfArray[i] * (10 - i);

        int digit1 = 11 - sum1 % 11;
        if (digit1 >= 10) digit1 = 0;
        if (cpfArray[9] != digit1) return false;

        int sum2 = 0;
        for (int i = 0; i < 10; i++) sum2 += cpfArray[i] * (11 - i);

        int digit2 = 11 - sum2 % 11;
        if (digit2 >= 10) digit2 = 0;

        return cpfArray[10] == digit2;
    }

    private bool ValidateCNPJ(string cnpj)
    {
        if (cnpj.All(c => c == cnpj[0])) return false;

        int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] cnpjArray = cnpj.Select(c => int.Parse(c.ToString())).ToArray();

        int sum1 = 0;
        for (int i = 0; i < 12; i++) sum1 += cnpjArray[i] * multiplier1[i];

        int digit1 = sum1 % 11 < 2 ? 0 : 11 - sum1 % 11;
        if (cnpjArray[12] != digit1) return false;

        int sum2 = 0;
        for (int i = 0; i < 13; i++) sum2 += cnpjArray[i] * multiplier2[i];

        int digit2 = sum2 % 11 < 2 ? 0 : 11 - sum2 % 11;

        return cnpjArray[13] == digit2;
    }
}
