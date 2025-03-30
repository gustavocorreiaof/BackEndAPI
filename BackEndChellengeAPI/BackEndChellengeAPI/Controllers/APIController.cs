using Core.BusinesseRules;
using Core.DTOs;
using Core.Entities;
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
            UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email);

            UserBR.InsertUser(userDTO);

            return Ok(ApiMsg.INF001);
        }

        /*[HttpPost("SendTransaction")]
        public IActionResult SendTransaction([FromBody] CreateUserRequest request)
        {
            UserDTO userDTO = new(request.Name, request.Password, request.TaxNumber, request.Email);

            UserBR.InsertUser(userDTO);

            ret
            n Ok(ApiMsg.INF001);
        }*/
    }
}
