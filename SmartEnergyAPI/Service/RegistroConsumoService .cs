using Microsoft.EntityFrameworkCore;
using SmartEnergyAPI.DTOs;
using SmartEnergyAPI.Models;
public interface IRegistroConsumoService
{
    Task<RegistroConsumoDTO> CreateRegistroConsumoAsync(CreateRegistroConsumoDTO registroDTO);
    Task<List<RegistroConsumoDTO>> GetRegistrosConsumoAsync();
}
public class RegistroConsumoService : IRegistroConsumoService
{
    private readonly PlataformaEnergeticaContext _context;

    public RegistroConsumoService(PlataformaEnergeticaContext context)
    {
        _context = context;
    }

    // Crear un nuevo registro de consumo
    public async Task<RegistroConsumoDTO> CreateRegistroConsumoAsync(CreateRegistroConsumoDTO registroDTO)
    {
        var usuario = await _context.Usuarios.FindAsync(registroDTO.UsuarioId);
        var dispositivo = await _context.Dispositivos.FindAsync(registroDTO.DispositivoId);

        if (usuario == null || dispositivo == null)
        {
            return null;  // O lanzar una excepción si prefieres manejarlo de otra forma
        }

        var registroConsumo = new RegistroConsumo
        {
            UsuarioId = registroDTO.UsuarioId,
            DispositivoId = registroDTO.DispositivoId,
            FechaRegistro = registroDTO.FechaRegistro ?? DateTime.Now,
            ConsumoKwh = registroDTO.ConsumoKwh
        };

        _context.RegistroConsumos.Add(registroConsumo);
        await _context.SaveChangesAsync();

        // Mapear a DTO para devolver la respuesta
        return new RegistroConsumoDTO
        {
            ConsumoId = registroConsumo.ConsumoId,
            UsuarioId = registroConsumo.UsuarioId,
            DispositivoId = registroConsumo.DispositivoId,
            FechaRegistro = registroConsumo.FechaRegistro,
            ConsumoKwh = registroConsumo.ConsumoKwh,
            Usuario = new UsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,               
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario
            },
            Dispositivo = new DispositivoDTO
            {
                DispositivoId = dispositivo.DispositivoId,
                Nombre = dispositivo.Nombre,
                Tipo = dispositivo.Tipo
            }
        };
    }

    // Obtener registros de consumo
    public async Task<List<RegistroConsumoDTO>> GetRegistrosConsumoAsync()
    {
        var registros = await _context.RegistroConsumos
            .Include(rc => rc.Usuario)
            .Include(rc => rc.Dispositivo)
            .ToListAsync();

        var registrosDTO = registros.Select(rc => new RegistroConsumoDTO
        {
            ConsumoId = rc.ConsumoId,
            UsuarioId = rc.UsuarioId,
            DispositivoId = rc.DispositivoId,
            FechaRegistro = rc.FechaRegistro,
            ConsumoKwh = rc.ConsumoKwh,
            Usuario = new UsuarioDTO
            {
                UsuarioId = rc.Usuario.UsuarioId,                
                Email = rc.Usuario.Email,
                TipoUsuario = rc.Usuario.TipoUsuario
            },
            Dispositivo = new DispositivoDTO
            {
                DispositivoId = rc.Dispositivo.DispositivoId,
                Nombre = rc.Dispositivo.Nombre,
                Tipo = rc.Dispositivo.Tipo
            }
        }).ToList();

        return registrosDTO;
    }
}
