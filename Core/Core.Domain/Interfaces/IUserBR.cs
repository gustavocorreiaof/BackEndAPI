using Core.Domain.DTOs;

namespace Core.Domain.Interfaces
{
    public interface IUserBR
    {
        void SaveUser(UserDTO user);
    }
}
