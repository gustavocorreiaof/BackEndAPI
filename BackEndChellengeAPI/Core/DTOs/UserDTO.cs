namespace Core.DTOs
{
    public class UserDTO
    {
        public UserDTO(string name, string password, string cpf, string email)
        {
            Name = name;
            Password = password;
            CPF = cpf;
            Email = email;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }


    }
}
