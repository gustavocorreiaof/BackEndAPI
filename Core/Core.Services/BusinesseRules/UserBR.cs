using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Core.Infrastructure.Mappers;
using Core.Infrastructure.Repository;

namespace Core.Services.BusinesseRules
{
    public class UserBR : IUserBR
    {
        public void SaveUser(UserDTO userDTO)
        {
            VerifyIfExistUser(userDTO.TaxNumber, userDTO.Email, userDTO.UserId);

            User user = (User)new ApiMapper().MapToEntityOrDTO(userDTO);

            if (userDTO.UserId == null)
                new UserRepository().InsertUser(user);
            else
                new UserRepository().UpdateUser(user);

        }

        public static List<User> GetAllUsers()
        {
            return new UserRepository().GetAllUsers();
        }

        private static void VerifyIfExistUser(string taxNumber, string email, long? userId)
        {
            var service = new UserRepository();

            var userByTax = service.GetUserByTaxNumber(taxNumber);
            if (userByTax != null && userByTax.Id != userId)
                throw new ApiException(ApiMsg.EX002);

            var userByEmail = service.GetUserByEmail(email);
            if (userByEmail != null && userByEmail.Id != userId)
                throw new ApiException(ApiMsg.EX003);
        }
    }
}

