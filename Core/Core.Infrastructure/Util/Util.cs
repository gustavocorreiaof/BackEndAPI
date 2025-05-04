namespace Core.Infrastructure.Util
{
    public static class Util
    {
        public static string RemoveSpecialCharacters(string taxNumber)
        {
            if (string.IsNullOrWhiteSpace(taxNumber))
                return string.Empty;

            return new string(taxNumber.Where(char.IsDigit).ToArray());
        }
    }
}
