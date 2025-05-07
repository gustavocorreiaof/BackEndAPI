using Core.Domain.Entities;

namespace Core.Infrastructure.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactionsByUserId(long userId, DateTime? startDate, DateTime? endDate);
        void PerformTransaction(User payerId, User payeeId, decimal transferValue);
    }
}
