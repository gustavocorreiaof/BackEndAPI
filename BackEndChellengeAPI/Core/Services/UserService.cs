using Core.Common.Entities;
using Core.Common.Repository;

namespace Core.Common.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository = new();

        public User GetUserByTaxNumber(string userTaxNumber)
        {
            return _userRepository.GetUserByTaxNumber(userTaxNumber);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public void InsertUser(User user)
        {
            _userRepository.InsertUser(user);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }
    }
}
