using Core.Domain.Entities;

namespace Core.Infrastructure.Events
{
    public class TransferEventArgs : EventArgs
    {
        public User Payer { get; set; }
        public User Payee { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
