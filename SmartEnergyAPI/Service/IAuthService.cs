using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartEnergyAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartEnergyAPI.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(Usuario user);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly PlataformaEnergeticaContext _context;

        public AuthService(IConfiguration configuration, PlataformaEnergeticaContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> GenerateJwtToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, user.TipoUsuario ?? "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
