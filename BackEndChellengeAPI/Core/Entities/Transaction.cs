using Core.Entities.Base;

namespace Core.Entities
{
    public class Transaction:BaseEntity
    {
        public User Payer { get; set; }
        public User Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
