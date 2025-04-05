using Core.Entities.Base;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string TaxNumber { get; set; }

        public string Email { get; set; }

        public UserType Type { get; set; }

        public User(long id, string name, string pwd, string cpf, string email, UserType userType)
        {
            this.Id = id;
            this.Name = name;
            this.Password = pwd;
            this.TaxNumber = cpf;
            this.Email = email;
            this.Type = userType;
        }

        public User(){}
    }
}
