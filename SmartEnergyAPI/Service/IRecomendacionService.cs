using Microsoft.EntityFrameworkCore;
using SmartEnergyAPI.DTOs;
using SmartEnergyAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IRecomendacionService
{
    Task<List<RecomendacioneDTO>> GetAllRecomendacionesAsync();
    Task<RecomendacioneDTO> GetRecomendacionByIdAsync(int id);
    Task<RecomendacioneDTO> CreateRecomendacionAsync(CreateRecomendacioneDTO recomendacionDTO);
    Task<RecomendacioneDTO> UpdateRecomendacionAsync(int id, UpdateRecomendacioneDTO recomendacionDTO); // Cambiado a UpdateRecomendacioneDTO
    Task<bool> DeleteRecomendacionAsync(int id);
}


public class RecomendacionService : IRecomendacionService
{
    private readonly PlataformaEnergeticaContext _context;

    public RecomendacionService(PlataformaEnergeticaContext context)
    {
        _context = context;
    }

    // Obtener todas las recomendaciones, incluyendo los detalles del usuario
    public async Task<List<RecomendacioneDTO>> GetAllRecomendacionesAsync()
    {
        var recomendaciones = await _context.Recomendaciones
            .Include(r => r.Usuario)  // Incluir solo el usuario
            .Select(r => new RecomendacioneDTO
            {
                RecomendacionId = r.RecomendacionId,
                UsuarioId = r.UsuarioId,
                FechaRecomendacion = r.FechaRecomendacion,
                TipoRecomendacion = r.TipoRecomendacion,
                Descripcion = r.Descripcion,
                AccionSugerida = r.AccionSugerida,
                Usuario = new UsuarioDTO
                {
                    UsuarioId = r.Usuario.UsuarioId,
                    Email = r.Usuario.Email,
                    TipoUsuario = r.Usuario.TipoUsuario
                }
            })
            .ToListAsync();

        return recomendaciones;
    }

    // Obtener una recomendación por id, incluyendo los detalles del usuario
    public async Task<RecomendacioneDTO> GetRecomendacionByIdAsync(int id)
    {
        var recomendacion = await _context.Recomendaciones
            .Include(r => r.Usuario)  // Incluir los detalles del Usuario
            .FirstOrDefaultAsync(r => r.RecomendacionId == id);

        if (recomendacion == null) return null;

        return new RecomendacioneDTO
        {
            RecomendacionId = recomendacion.RecomendacionId,
            UsuarioId = recomendacion.UsuarioId,
            FechaRecomendacion = recomendacion.FechaRecomendacion,
            TipoRecomendacion = recomendacion.TipoRecomendacion,
            Descripcion = recomendacion.Descripcion,
            AccionSugerida = recomendacion.AccionSugerida,
            Usuario = new UsuarioDTO
            {
                UsuarioId = recomendacion.Usuario.UsuarioId,
                Email = recomendacion.Usuario.Email,
                TipoUsuario = recomendacion.Usuario.TipoUsuario
            }
        };
    }

    // Crear una nueva recomendación
    public async Task<RecomendacioneDTO> CreateRecomendacionAsync(CreateRecomendacioneDTO recomendacionDTO)
    {
        var recomendacion = new Recomendacione
        {
            UsuarioId = recomendacionDTO.UsuarioId,
            FechaRecomendacion = recomendacionDTO.FechaRecomendacion,
            TipoRecomendacion = recomendacionDTO.TipoRecomendacion,
            Descripcion = recomendacionDTO.Descripcion,
            AccionSugerida = recomendacionDTO.AccionSugerida
        };

        _context.Recomendaciones.Add(recomendacion);
        await _context.SaveChangesAsync();

        return new RecomendacioneDTO
        {
            RecomendacionId = recomendacion.RecomendacionId,
            UsuarioId = recomendacion.UsuarioId,
            FechaRecomendacion = recomendacion.FechaRecomendacion,
            TipoRecomendacion = recomendacion.TipoRecomendacion,
            Descripcion = recomendacion.Descripcion,
            AccionSugerida = recomendacion.AccionSugerida
        };
    }

    public async Task<RecomendacioneDTO> UpdateRecomendacionAsync(int id, UpdateRecomendacioneDTO recomendacionDTO)
    {
        var existingRecomendacion = await _context.Recomendaciones.FindAsync(id);
        if (existingRecomendacion == null) return null;

        // Actualizar los campos de la entidad Recomendacione con los datos del DTO
        existingRecomendacion.FechaRecomendacion = recomendacionDTO.FechaRecomendacion;
        existingRecomendacion.TipoRecomendacion = recomendacionDTO.TipoRecomendacion;
        existingRecomendacion.Descripcion = recomendacionDTO.Descripcion;
        existingRecomendacion.AccionSugerida = recomendacionDTO.AccionSugerida;

        await _context.SaveChangesAsync();

        // Mapear la entidad actualizada a un DTO antes de devolverla
        return new RecomendacioneDTO
        {
            RecomendacionId = existingRecomendacion.RecomendacionId,
            UsuarioId = existingRecomendacion.UsuarioId,
            FechaRecomendacion = existingRecomendacion.FechaRecomendacion,
            TipoRecomendacion = existingRecomendacion.TipoRecomendacion,
            Descripcion = existingRecomendacion.Descripcion,
            AccionSugerida = existingRecomendacion.AccionSugerida,
            Usuario = new UsuarioDTO
            {
                UsuarioId = existingRecomendacion.Usuario.UsuarioId,
                Email = existingRecomendacion.Usuario.Email,
                TipoUsuario = existingRecomendacion.Usuario.TipoUsuario
            }
        };
    }




    // Eliminar una recomendación
    public async Task<bool> DeleteRecomendacionAsync(int id)
    {
        var recomendacion = await _context.Recomendaciones.FindAsync(id);
        if (recomendacion == null) return false;

        _context.Recomendaciones.Remove(recomendacion);
        await _context.SaveChangesAsync();
        return true;
    }
}
