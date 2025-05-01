using Core.Domain.DTOs;

namespace Core.Domain.Interfaces
{
    public interface IUserBR
    {
        long SaveUser(UserDTO user);
    }
}
