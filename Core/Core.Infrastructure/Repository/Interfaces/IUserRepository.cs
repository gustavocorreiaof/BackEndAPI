using Core.Domain.Entities;

namespace Core.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        User GetById(long id);
        User GetByEmail(string email);
        User GetByTaxNumber(string taxnumber);
        List<User> GetAllUsers();
        long Insert(User user);
        long Update(User user);
        void Delete(User user);
        decimal GetUserBalance(long userId);
    }
}
