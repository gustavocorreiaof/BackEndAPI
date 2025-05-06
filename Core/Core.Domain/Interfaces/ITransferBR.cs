using Core.Domain.DTOs;

namespace Core.Domain.Interfaces
{
    public interface ITransferBR
    {
        Task PerformTransactionAsync(TransferDTO dto);
    }
}
