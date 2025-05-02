using Core.Domain.Entities;

namespace Core.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        public long Insert(User user);
        public long Update(User user);
        public void Delete(User user);
        public User GetById(long userId);
        public User GetByTaxNumber(string userTaxNumber);
        public User GetByEmail(string email);
        public List<User> GetAllUsers();
    }
}
