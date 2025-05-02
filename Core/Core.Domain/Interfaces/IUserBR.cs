using Core.Domain.DTOs;
using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IUserBR
    {
        long SaveUser(UserDTO user);
        void DeleteUser(long userId);
        List<User> GetAllUsers();
        void UpdateName(long userId, string newName);
        void UpdateEmail(long userId, string newEmail);
        void UpdatePassword(long userId, string newPassword);
    }
}
