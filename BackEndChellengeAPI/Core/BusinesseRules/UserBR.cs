using Core.Common.DTOs;
using Core.Common.Entities;
using Core.Common.Exceptions;
using Core.Common.Interfaces;
using Core.Common.Mappers;
using Core.Common.Services;
using Core.Common.Util.Msgs;

namespace Core.Common.BusinesseRules
{
    public class UserBR : IUserBR
    {
        public void SaveUser(UserDTO userDTO)
        {
            VerifyIfExistUser(userDTO.TaxNumber, userDTO.Email, userDTO.UserId);

            User user = (User)new ApiMapper().MapToEntityOrDTO(userDTO);

            if (userDTO.UserId == null)
                new UserService().InsertUser(user);
            else
                new UserService().UpdateUser(user);

        }

        public static List<User> GetAllUsers()
        {
            return new UserService().GetAllUsers();
        }

        private static void VerifyIfExistUser(string taxNumber, string email, long? userId)
        {
            var service = new UserService();

            var userByTax = service.GetUserByTaxNumber(taxNumber);
            if (userByTax != null && userByTax.Id != userId)
                throw new ApiException(ApiMsg.EX002);

            var userByEmail = service.GetUserByEmail(email);
            if (userByEmail != null && userByEmail.Id != userId)
                throw new ApiException(ApiMsg.EX003);
        }
    }
}

