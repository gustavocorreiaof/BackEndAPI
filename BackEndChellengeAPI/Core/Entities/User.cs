namespace Core.Entities
{
    public class User
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public User(string name, string pwd, string cpf, string email)
        {
            this.Name = name;
            this.Password = pwd;
            this.CPF = cpf;
            this.Email = email;
        }
    }
}
