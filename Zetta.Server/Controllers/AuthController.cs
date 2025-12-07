using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zetta.BD.DATA;
using Zetta.Shared.DTOS;

namespace Zetta.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AuthController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<SesionDTO>> Login(LoginDTO login)
        {
            // 1. Buscamos si existe un usuario con ese Nombre y ese DNI
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreU.ToLower() == login.NombreU.ToLower()
                                       && u.DNI == login.DNI);

            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o DNI incorrectos.");
            }

            // 2. Generar el Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreU)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30), //tiempo que dura el logueo
                signingCredentials: creds
            );

            return Ok(new SesionDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                NombreU = usuario.NombreU
            });
        }
    }
}