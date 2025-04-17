using BackEndChellengeAPI.Requests;
using Core.Domain.DTOs;
using Core.Domain.Msgs;
using Core.Services.BusinesseRules;
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
