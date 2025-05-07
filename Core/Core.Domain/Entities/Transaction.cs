using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    public class Transaction: BaseEntity
    {
        public long PayerId { get; set; }
        public long PayeeId { get; set; }
        public decimal TransferValue { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
