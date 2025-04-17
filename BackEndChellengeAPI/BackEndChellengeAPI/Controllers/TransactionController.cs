using Core.BusinesseRules;
using Core.DTOs;
using Core.Requests;
using Core.Util.Msgs;
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

            return Ok(ApiMsg.INF002);
        }
    }
}
