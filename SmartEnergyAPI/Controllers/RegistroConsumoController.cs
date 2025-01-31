using Microsoft.AspNetCore.Mvc;
using SmartEnergyAPI.DTOs;

[ApiController]
[Route("api/[controller]")]
public class RegistroConsumoController : ControllerBase
{
    private readonly IRegistroConsumoService _registroConsumoService;

    public RegistroConsumoController(IRegistroConsumoService registroConsumoService)
    {
        _registroConsumoService = registroConsumoService;
    }

    // Endpoint para crear un nuevo registro de consumo
    [HttpPost]
    public async Task<IActionResult> CreateRegistroConsumo([FromBody] CreateRegistroConsumoDTO registroDTO)
    {
        if (registroDTO.UsuarioId == null || registroDTO.DispositivoId == null)
        {
            return BadRequest("El UsuarioId y DispositivoId son obligatorios.");
        }

        var registroCreado = await _registroConsumoService.CreateRegistroConsumoAsync(registroDTO);

        if (registroCreado == null)
        {
            return StatusCode(500, "Hubo un error al crear el registro de consumo.");
        }

        return CreatedAtAction(nameof(GetRegistrosConsumo), new { id = registroCreado.ConsumoId }, registroCreado);
    }

    // Endpoint para obtener todos los registros de consumo
    [HttpGet]
    public async Task<IActionResult> GetRegistrosConsumo()
    {
        var registros = await _registroConsumoService.GetRegistrosConsumoAsync();
        return Ok(registros);
    }
}
