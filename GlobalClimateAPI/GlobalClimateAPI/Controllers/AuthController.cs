using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Msgs;
using Core.Infrastructure.Data.Interfaces;
using GlobalClimateAPI.Requests;
using GlobalClimateAPI.Responses;
using GlobalClimateAPI.Responses.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlobalClimateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDynamoDbContext _dbContext;

        public AuthController(IDynamoDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerOperation(Summary = "Verify if a user exists in Dynamo and generate a JWT token based on their credentials.", Description = "Returns a JWT for that user.")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                User user = await _dbContext.GetUserByNameAsync(request.UserName) ?? throw new ApiException(ApiMsgs.EXC001);

                if (user.Password != request.Password)
                    return Unauthorized(new BaseResponse { Success = true, Message = ApiMsgs.EXC002 });

                var token = GenerateJwtToken(user.Name, user.Email);

                return Ok(new LoginResponse { Success = true, Token = token });
            }
            catch (ApiException ex)
            {
                return BadRequest(new BaseResponse() { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ApiMsgs.INF004,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        private string GenerateJwtToken(string username, string email)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
