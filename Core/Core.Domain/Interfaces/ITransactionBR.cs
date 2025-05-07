using Core.Domain.DTOs;
using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface ITransactionBR
    {
        List<Transaction> GetTransactionsByUserId(long userId, DateTime? startDate, DateTime? endDate);
        Task PerformTransactionAsync(TransferDTO dto);
    }
}
