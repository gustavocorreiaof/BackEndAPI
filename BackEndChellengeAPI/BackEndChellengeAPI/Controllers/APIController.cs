using Core.BusinesseRules;
using Core.DTOs;
using Core.Exceptions;
using Core.Requests;
using Core.Util.Msgs;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        [HttpPost("InsertUser")]
        public IActionResult InsertUser([FromBody] CreateUserRequest request)
        {
            UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email, request.UserType);

            UserBR.InsertUser(userDTO);

            return Ok(ApiMsg.INF001);
        }

        [HttpPost("SendTransaction")]
        public async Task<IActionResult> SendTransaction([FromBody] SendTransactionRequest request)
        {
            try
            {
                TransferDTO transferDTO = new(request.PayerTaxNumber, request.PayeeTaxNumber, request.TransferValue);

                await TransferBR.PerformTransactionAsync(transferDTO);

                return Ok("Transferência efetuada com sucesso");
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
