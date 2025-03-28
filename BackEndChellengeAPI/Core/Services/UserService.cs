using Core.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository = new();

        public User GetUserByTaxNumber(string userTaxNumber)
        {
            return _userRepository.GetUserByTaxNumber(userTaxNumber);
        }

        public object GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public void InsertUser(User user)
        {
            _userRepository.InsertUser(user);
        }
    }
}
