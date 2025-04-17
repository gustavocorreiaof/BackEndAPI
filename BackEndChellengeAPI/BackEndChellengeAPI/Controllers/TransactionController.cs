using Core.API.Requests;
using Core.Common.BusinesseRules;
using Core.Common.DTOs;
using Core.Common.Util.Msgs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
