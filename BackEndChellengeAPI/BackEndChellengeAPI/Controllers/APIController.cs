using Core.BusinesseRules;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Requests;
using Core.Services;
using Core.Util.Msgs;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private readonly IUserBR _userService;

        public APIController(IUserBR userService)
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

            new UserBR().CreateOrUpdateUser(userDTO);

            return Ok(ApiMsg.INF001);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            UserDTO userDTO = new UserDTO(request.NewName, request.NewPassword, request.NewTaxNumber, request.NewEmail, userType: null, request.UserId);
            new UserBR().CreateOrUpdateUser(userDTO);

            return Ok("");
        }

        [HttpPost("SendTransaction")]
        public async Task<IActionResult> SendTransaction([FromBody] SendTransactionRequest request)
        {
            try
            {
                TransferDTO transferDTO = new(request.PayerTaxNumber, request.PayeeTaxNumber, request.TransferValue);

                await TransferBR.PerformTransactionAsync(transferDTO);

                var notificationService = new NotificationBR();

                bool notificationpublished = await notificationService.SendNotificationAsync(transferDTO.PayeeEmail, "Your payment has been received successfully.");

                if (!notificationpublished)
                    return StatusCode(206, "Transfer partially completed. Notification has not sended.");

                return Ok("Transfer completed successfully.");
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
