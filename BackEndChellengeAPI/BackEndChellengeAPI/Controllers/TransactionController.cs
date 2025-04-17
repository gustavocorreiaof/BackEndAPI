using Core.BusinesseRules;
using Core.DTOs;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        [HttpPost("SendTransaction")]
        public async Task<IActionResult> SendTransaction([FromBody] SendTransactionRequest request)
        {
            TransferDTO transferDTO = new(request.PayerTaxNumber, request.PayeeTaxNumber, request.TransferValue);

            await TransferBR.PerformTransactionAsync(transferDTO);

            /*var notificationService = new NotificationBR();

            bool notificationpublished = await notificationService.SendNotificationAsync(transferDTO.PayeeEmail, "Your payment has been received successfully.");

            if (!notificationpublished)
                return StatusCode(206, "Transfer partially completed. Notification has not sended.");*/

            return Ok("Transfer completed successfully.");
        }
    }
}
