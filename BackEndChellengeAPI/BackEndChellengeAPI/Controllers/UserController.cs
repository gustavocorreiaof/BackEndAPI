using BackEndChellengeAPI.Requests;
using BackEndChellengeAPI.Requests.Base;
using BackEndChellengeAPI.Responses;
using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Core.Infrastructure.Util;
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

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<User> users = _userBR.GetAllUsers();
            return Ok(new ApiResponse<List<User>> { Data = users });
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] long id)
        {
            User user = _userBR.GetById(id);
            return Ok(new ApiResponse<User> { Data = user });
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] CreateUserRequest request)
        {
            UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email, request.UserType, userId: null);

            userDTO.TaxNumber = Util.RemoveSpecialCharacters(userDTO.TaxNumber);

            long userId = _userBR.SaveUser(userDTO);

            return Ok(new ApiResponse<long> { Message = ApiMsg.INF001, Data = userId });
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            UserDTO userDTO = new UserDTO(request.NewName, request.NewPassword, Util.RemoveSpecialCharacters(request.NewTaxNumber), request.NewEmail, userType: null, request.UserId);
            long userId = _userBR.SaveUser(userDTO);

            return Ok(new { Message = ApiMsg.INF006, UserId = userId });
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] BaseRequest request)
        {
            _userBR.DeleteUser(request.UserId);

            return Ok(new { Message = ApiMsg.INF007 });
        }

        [HttpPatch("UpdateName")]
        public IActionResult UpdateName([FromBody] PatchUpdateRequest request)
        {
            _userBR.UpdateName(request.UserId, request.Value);

            return Ok(new { Message = ApiMsg.INF008 });
        }

        [HttpPatch("UpdateEmail")]
        public IActionResult UpdateEmail([FromBody] PatchUpdateRequest request)
        {
            _userBR.UpdateEmail(request.UserId, request.Value);

            return Ok(new { ApiMsg.INF010 });
        }

        [HttpPatch("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] PatchUpdateRequest request)
        {
            _userBR.UpdatePassword(request.UserId, request.Value);

            return Ok(new { Message = ApiMsg.INF009 });
        }

        [HttpPatch("AddBalance")]
        public IActionResult AddBalance([FromBody] PatchUpdateRequest request)
        {
            if (!decimal.TryParse(request.Value, out decimal valueToBeAdded))
                throw new ApiException(ApiMsg.EX011);

            _userBR.AddBalance(request.UserId, valueToBeAdded);

            return Ok(new { Message = ApiMsg.INF014 });
        }

        [HttpGet("GetUserBalance")]
        public IActionResult GetUserBalance([FromQuery] long userId)
        {
            decimal userBalance = _userBR.GetUserBalance(userId);

            return Ok(new ApiResponse<decimal>()
            {
                Message = ApiMsg.INF011,
                Data = userBalance
            });
        }
    }
}
