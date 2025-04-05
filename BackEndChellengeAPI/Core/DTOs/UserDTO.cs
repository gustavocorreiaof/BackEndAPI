using Core.Entities;

namespace Core.DTOs
{
    public class UserDTO
    {
        public UserDTO(string name, string password, string taxNumber, string email, UserType userType)
        {
            Name = name;
            Password = password;
            TaxNumber = taxNumber;
            Email = email;
            Type = userType;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}
