using BackEndChellengeAPI.Requests;
using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Core.Services.BusinesseRules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserBR _userService;

        public UserController(IUserBR userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<User> users = UserBR.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("InsertUser")]
        public IActionResult InsertUser([FromBody] CreateUserRequest request)
        {
            UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email, request.UserType, userId: null);

            userDTO.TaxNumber = RemoveSpecialCharacters(userDTO.TaxNumber);

            new UserBR().SaveUser(userDTO);

            return Ok(ApiMsg.INF001);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            UserDTO userDTO = new UserDTO(request.NewName, request.NewPassword, request.NewTaxNumber, request.NewEmail, userType: null, request.UserId);
            new UserBR().SaveUser(userDTO);

            return Ok("");
        }

        private static string RemoveSpecialCharacters(string taxNumber)
        {
            if (string.IsNullOrWhiteSpace(taxNumber))
                return string.Empty;

            return new string(taxNumber.Where(char.IsDigit).ToArray());
        }
    }
}
