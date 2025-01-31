using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEnergyAPI.Models;
using SmartEnergyAPI.DTOs;
using SmartEnergyAPI.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PlataformaEnergeticaContext _context;
        private readonly IAuthService _authService;

        public AuthController(PlataformaEnergeticaContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Verificar si el usuario ya existe
            var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("El correo ya está registrado.");

            // Crear nuevo usuario
            var newUser = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(request.Contraseña), 
                FechaRegistro = DateTime.Now,
                TipoUsuario = request.TipoUsuario
            };

            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado correctamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Verificar si el usuario existe
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Contraseña, user.Contraseña))
                return Unauthorized("Correo o contraseña incorrectos.");

            // Generar token JWT
            var token = await _authService.GenerateJwtToken(user);

            return Ok(new AuthResponse { Token = token });
        }
    }
}
