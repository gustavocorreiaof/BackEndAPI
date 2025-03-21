using System.ComponentModel.DataAnnotations;

namespace Core.Util.Attributes
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        public CpfValidationAttribute() : base("O CPF informado é inválido.")
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var cpf = value.ToString();

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            int sum1 = 0;
            for (int i = 0; i < 9; i++)
            {
                sum1 += cpfArray[i] * (10 - i);
            }

            int digit1 = 11 - (sum1 % 11);
            digit1 = digit1 >= 10 ? 0 : digit1;

            if (cpfArray[9] != digit1)
                return false;

            int sum2 = 0;
            for (int i = 0; i < 10; i++)
            {
                sum2 += cpfArray[i] * (11 - i);
            }

            int digit2 = 11 - (sum2 % 11);
            digit2 = digit2 >= 10 ? 0 : digit2;

            return cpfArray[10] == digit2;
        }
    }
}
