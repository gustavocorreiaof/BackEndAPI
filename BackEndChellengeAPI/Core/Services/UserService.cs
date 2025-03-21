using Core.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository = new();

        public UserService()
        {
        }

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByCPF(string userCPF)
        {
            return _userRepository.GetUserByCPF(userCPF);
        }
    }
}
