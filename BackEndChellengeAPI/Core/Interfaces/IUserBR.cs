using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUserBR
    {
        void CreateOrUpdateUser(UserDTO user);
    }
}
