namespace SmartEnergyAPI.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }

    public class RegisterRequest
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; } 
    }

    public class AuthResponse
    {
        public string Token { get; set; }
    }
    public class RecomendacioneDTO
    {
        public int RecomendacionId { get; set; }

        public int? UsuarioId { get; set; }

        public DateTime? FechaRecomendacion { get; set; }

        public string? TipoRecomendacion { get; set; }

        public string? Descripcion { get; set; }

        public string? AccionSugerida { get; set; }

        // No es necesario incluir Usuario completo, solo el Id o los datos relevantes
        public UsuarioDTO? Usuario { get; set; }
    }
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
    }
    public class CreateRecomendacioneDTO
    {
        public int? UsuarioId { get; set; }

        public DateTime? FechaRecomendacion { get; set; }

        public string? TipoRecomendacion { get; set; }

        public string? Descripcion { get; set; }

        public string? AccionSugerida { get; set; }
    }
    public class UpdateRecomendacioneDTO
    {
        public int RecomendacionId { get; set; }

        public int? UsuarioId { get; set; }

        public DateTime? FechaRecomendacion { get; set; }

        public string? TipoRecomendacion { get; set; }

        public string? Descripcion { get; set; }

        public string? AccionSugerida { get; set; }
    }
    public class CreateRegistroConsumoDTO
    {
        public int? UsuarioId { get; set; }
        public int? DispositivoId { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public decimal? ConsumoKwh { get; set; }
    }
    public class RegistroConsumoDTO
    {
        public int ConsumoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? DispositivoId { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public decimal? ConsumoKwh { get; set; }

        public UsuarioDTO Usuario { get; set; }  // Información del usuario
        public DispositivoDTO Dispositivo { get; set; }  // Información del dispositivo
    }

   
    public class DispositivoDTO
    {
        public int DispositivoId { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
    }


}
