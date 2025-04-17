using Core.API.Requests;
using Core.Common.BusinesseRules;
using Core.Common.DTOs;
using Core.Common.Entities;
using Core.Common.Interfaces;
using Core.Common.Util.Msgs;
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
    }
}
