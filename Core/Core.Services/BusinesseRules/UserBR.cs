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
        public long SaveUser(UserDTO userDTO)
        {
            VerifyIfExistUser(userDTO.TaxNumber, userDTO.Email, userDTO.UserId);

            userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            User user = (User)new ApiMapper().MapToEntityOrDTO(userDTO);

            if (userDTO.UserId == null)
                 return new UserRepository().Insert(user);
            else
                 return new UserRepository().Update(user);

        }

        public List<User> GetAllUsers()
        {
            return new UserRepository().GetAllUsers();
        }
        
        public void DeleteUser(long userId)
        {
            UserRepository service = new UserRepository();

            User user = service.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            service.Delete(user);
        }

        private void VerifyIfExistUser(string taxNumber, string email, long? userId)
        {
            UserRepository service = new UserRepository();

            var userByTax = service.GetByTaxNumber(taxNumber);
            if (userByTax != null && userByTax.Id != userId)
                throw new ApiException(ApiMsg.EX002);

            var userByEmail = service.GetByEmail(email);
            if (userByEmail != null && userByEmail.Id != userId)
                throw new ApiException(ApiMsg.EX003);
        }
    }
}

