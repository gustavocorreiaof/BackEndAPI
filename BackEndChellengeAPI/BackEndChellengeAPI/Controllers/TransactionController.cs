using BackEndChellengeAPI.Requests;
using BackEndChellengeAPI.Responses;
using Core.Domain.DTOs;
using Core.Domain.Entities;
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
        public readonly ITransactionBR _transferBR;

        public TransactionController(ITransactionBR transferBR)
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

        [HttpGet("GetTransactionsByUserId")]
        public IActionResult GetTransactionsByUserId([FromQuery] long userId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            List<Transaction> transactions = _transferBR.GetTransactionsByUserId(userId, startDate, endDate);

            if (transactions == null || !transactions.Any())
                return NotFound(new ApiResponse<string>() { Message = ApiMsg.INF012 });

            return Ok(new ApiResponse<List<Transaction>>() { Message = string.Format(ApiMsg.INF013, userId), Data = transactions });
        }
    }
}
