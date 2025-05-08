using Core.Domain.Entities;

namespace Core.Infrastructure.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccountByUserId(long userId);
        long InsertAccount(User user);
    }
}
