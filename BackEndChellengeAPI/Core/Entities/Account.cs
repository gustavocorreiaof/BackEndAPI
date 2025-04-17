using Core.Common.Entities.Base;

namespace Core.Common.Entities
{
    public class Account : BaseEntity
    {
        public Account(User user, decimal balance)
        {
            User = user;
            Balance = balance;
        }
        public Account() { }

        public User User { get; set; }
        public decimal Balance { get; set; }
    }
}
