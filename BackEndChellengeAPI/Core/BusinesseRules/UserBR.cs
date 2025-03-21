using Core.DTOs;
using Core.Entities;
using Core.Services;

namespace Core.BusinesseRules
{
    public static class UserBR
    {
        public static void InsertUser(UserDTO userDTO)
        {
			try
			{
                User user = new UserService().GetUserByCPF(userCPF: userDTO.CPF);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}

