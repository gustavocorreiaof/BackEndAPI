using BackEndChellengeAPI.Requests;
using BackEndChellengeAPI.Requests.Base;
using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserBR _userBR;

        public UserController(IUserBR userBR)
        {
            _userBR = userBR;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<User> users = _userBR.GetAllUsers();
            return Ok(new { Success = true, Users = users });
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] CreateUserRequest request)
        {
            UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email, request.UserType, userId: null);

            userDTO.TaxNumber = RemoveSpecialCharacters(userDTO.TaxNumber);

            long userId = _userBR.SaveUser(userDTO);

            return Ok(new { Success = true, Message = ApiMsg.INF001, UserId = userId });
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            UserDTO userDTO = new UserDTO(request.NewName, request.NewPassword, request.NewTaxNumber, request.NewEmail, userType: null, request.UserId);
            long userId = _userBR.SaveUser(userDTO);

            return Ok(new { Success = true, Message = ApiMsg.INF006, UserId = userId });
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] BaseRequest request)
        {
            _userBR.DeleteUser(request.UserId);

            return Ok(new { Success = true, Message = ApiMsg.INF007 });
        }

        [HttpPatch("UpdateName")]
        public IActionResult UpdateName([FromBody] PatchUpdateRequest request)
        {
            _userBR.UpdateName(request.UserId, request.Value);

            return Ok(new { Success = true, Message = ApiMsg.INF008 });
        }

        [HttpPatch("UpdateEmail")]
        public IActionResult UpdateEmail([FromBody] PatchUpdateRequest request)
        {
            _userBR.UpdateEmail(request.UserId, request.Value);

            return Ok(new { Success = true, ApiMsg.INF010 });
        }

        [HttpPatch("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] PatchUpdateRequest request)
        {
            _userBR.UpdatePassword(request.UserId, request.Value);

            return Ok(new { Success = true, Message = ApiMsg.INF009 });
        }

        private static string RemoveSpecialCharacters(string taxNumber)
        {
            if (string.IsNullOrWhiteSpace(taxNumber))
                return string.Empty;

            return new string(taxNumber.Where(char.IsDigit).ToArray());
        }
    }
}
