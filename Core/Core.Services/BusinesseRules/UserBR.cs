using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Core.Infrastructure.Mappers;
using Core.Infrastructure.Repository.Interfaces;
using System.Text.RegularExpressions;

namespace Core.Services.BusinesseRules
{
    public class UserBR : IUserBR
    {
        public readonly IUserRepository _userRepository;

        public UserBR(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public long SaveUser(UserDTO userDTO)
        {
            VerifyIfExistUser(userDTO.TaxNumber, userDTO.Email, userDTO.UserId);

            userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            User user = (User)new ApiMapper().MapToEntityOrDTO(userDTO);

            if (userDTO.UserId == null)
            {
                user.CreationDate = DateTime.Now;
                return _userRepository.Insert(user);
            }
            else
            {
                user.UpdateDate = DateTime.Now;
                return _userRepository.Update(user);
            }

        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetById(long Id)
        {
            return _userRepository.GetById(Id);
        }
        
        public void DeleteUser(long userId)
        {
            User user = _userRepository.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            _userRepository.Delete(user);
        }

        public void UpdateName(long userId, string newName)
        {
            User user = _userRepository.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            user.Name = newName;
            user.UpdateDate = DateTime.Now;

            _userRepository.Update(user);
        }

        public void UpdateEmail(long userId, string newEmail)
        {
            User user = _userRepository.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            VerifyIfExistUser(taxNumber: null, newEmail, userId);

            user.Email = newEmail;
            user.UpdateDate = DateTime.Now;

            _userRepository.Update(user);
        }

        public void UpdatePassword(long userId, string newPassword)
        {
            User user = _userRepository.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            IsValidPassword(newPassword);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UpdateDate = DateTime.Now;

            _userRepository.Update(user);
        }

        private void VerifyIfExistUser(string? taxNumber, string? email, long? userId)
        {
            if (userId != null)
            {
                var user = _userRepository.GetById(userId.Value) ?? throw new ApiException(ApiMsg.EX009);
            }

            var userByTax = _userRepository.GetByTaxNumber(taxNumber);
            if (userByTax != null && userByTax.Id != userId)
                throw new ApiException(ApiMsg.EX002);

            var userByEmail = _userRepository.GetByEmail(email);
            if (userByEmail != null && userByEmail.Id != userId)
                throw new ApiException(ApiMsg.EX003);

        }

        private void IsValidPassword(string password)
        {
            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$";

            if(password == null || Regex.IsMatch(password, passwordRegex))
                throw new ApiException(ApiMsg.EX010);
        }

        public decimal GetUserBalance(long userId)
        {
            User user = _userRepository.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            return _userRepository.GetUserBalance(user.Id);
        }

        public void AddBalance(long userId, decimal value)
        {
            User user = _userRepository.GetById(userId) ?? throw new ApiException(ApiMsg.EX009);

            _userRepository.AddBalance(user.Id, value);
        }
    }
}

