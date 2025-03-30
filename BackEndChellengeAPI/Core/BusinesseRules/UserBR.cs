using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Mappers;
using Core.Services;
using Core.Util.Msgs;

namespace Core.BusinesseRules
{
    public static class UserBR
    {
        public static void InsertUser(UserDTO userDTO)
        {
            User? user = new UserService().GetUserByTaxNumber(userDTO.TaxNumber) switch
            {
                not null => throw new ApiException(ApiMsg.EX002),
                _ => null
            };

            user = new UserService().GetUserByEmail(userDTO.TaxNumber) switch
            {
                not null => throw new ApiException(ApiMsg.EX003),
                _ => new User()
            };

            user = (User)new ApiMapper().MapToEntityOrDTO(userDTO);

            new UserService().InsertUser(user);
        }
    }
}

