namespace Core.DTOs
{
    public class UserDTO
    {
        public UserDTO(string name, string password, string taxNumber, string email)
        {
            Name = name;
            Password = password;
            TaxNumber = taxNumber;
            Email = email;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
    }
}
