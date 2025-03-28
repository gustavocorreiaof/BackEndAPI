using Core.Entities.Base;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string TaxNumber { get; set; }

        public string Email { get; set; }

        public User(string name, string pwd, string cpf, string email)
        {
            this.Name = name;
            this.Password = pwd;
            this.TaxNumber = cpf;
            this.Email = email;
        }

        public User(){}
    }
}
