using BackEndChellengeAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEndChellengeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }/*

        [HttpPost]
        public IActionResult Login([FromBody] CreateTokenRequest request)
        {
            var token = GenerateJwtToken(request.cpf, request.senha);
            return Ok(new { token });
        }*/

        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string cpf, [FromQuery] string senha)
        {
            string cpfValido = "48072729004";
            string senhaValida = "123456";

            try
            {
                if(cpf != cpfValido || senha != senhaValida)
                    return Unauthorized("Login invalido.");
                
                return Ok("Login bem sucedido!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateJwtToken(string username, string senha)
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
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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
