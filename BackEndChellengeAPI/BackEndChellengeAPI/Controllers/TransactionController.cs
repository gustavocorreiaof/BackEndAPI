using BackEndChellengeAPI.Requests;
using BackEndChellengeAPI.Responses;
using Core.Domain.DTOs;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        public readonly ITransferBR _transferBR;

        public TransactionController(ITransferBR transferBR)
        {
            _transferBR = transferBR;
        }

        [HttpPost("SendTransaction")]
        public async Task<IActionResult> SendTransaction([FromBody] SendTransactionRequest request)
        {
            TransferDTO transferDTO = new(request.PayerTaxNumber, request.PayeeTaxNumber, request.TransferValue);

            await _transferBR.PerformTransactionAsync(transferDTO);           

            return Ok(new ApiResponse<int>() { Message = ApiMsg.INF002 });
        }
    }
}
