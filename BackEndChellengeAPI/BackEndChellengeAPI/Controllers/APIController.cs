using Core.BusinesseRules;
using Core.DTOs;
using Core.Exceptions;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {

        private readonly ILogger<APIController> _logger;

        public APIController(ILogger<APIController> logger)
        {
            _logger = logger;
        }

        [HttpPost("insertuser")]
        public IActionResult InsertUser([FromBody] CreateUserRequest request)
        {
            try
            {
                UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email);

                UserBR.InsertUser(userDTO);

                return Ok("Usuário criado com sucesso.");
            }
            catch (ApiException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao criar o usuário");
            }
        }
    }
}
