using Core.Entities.Base;

namespace Core.Entities
{
    public class Account : BaseEntity
    {
        public Account(User user, decimal balance)
        {
            User = user;
            Balance = balance;
        }
        public Account(){}

        public User User { get; set; }
        public decimal Balance { get; set; }
    }
}
