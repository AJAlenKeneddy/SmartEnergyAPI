using Microsoft.AspNetCore.Mvc;
using SmartEnergyAPI.Models;
using SmartEnergyAPI.Services;
using SmartEnergyAPI.DTOs; // Asegúrate de incluir el espacio de nombres de los DTOs.

namespace SmartEnergyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendacionesController : ControllerBase
    {
        private readonly IRecomendacionService _recomendacionService;

        public RecomendacionesController(IRecomendacionService recomendacionService)
        {
            _recomendacionService = recomendacionService;
        }

        // GET: api/Recomendaciones
        [HttpGet]
        public async Task<ActionResult<List<RecomendacioneDTO>>> GetRecomendaciones()
        {
            var recomendaciones = await _recomendacionService.GetAllRecomendacionesAsync();
            return Ok(recomendaciones);
        }

        // GET: api/Recomendaciones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecomendacioneDTO>> GetRecomendacion(int id)
        {
            var recomendacion = await _recomendacionService.GetRecomendacionByIdAsync(id);

            if (recomendacion == null)
            {
                return NotFound();
            }

            return Ok(recomendacion);
        }

        // POST: api/Recomendaciones
        [HttpPost]
        public async Task<ActionResult<RecomendacioneDTO>> PostRecomendacion(CreateRecomendacioneDTO recomendacionDTO)
        {
            var createdRecomendacion = await _recomendacionService.CreateRecomendacionAsync(recomendacionDTO);
            return CreatedAtAction(nameof(GetRecomendacion), new { id = createdRecomendacion.RecomendacionId }, createdRecomendacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecomendacion(int id, UpdateRecomendacioneDTO recomendacionDTO)
        {
            if (id != recomendacionDTO.RecomendacionId)
            {
                return BadRequest();
            }

            var updatedRecomendacion = await _recomendacionService.UpdateRecomendacionAsync(id, recomendacionDTO);

            if (updatedRecomendacion == null)
            {
                return NotFound();
            }

            return NoContent();
        }



        // DELETE: api/Recomendaciones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomendacion(int id)
        {
            var success = await _recomendacionService.DeleteRecomendacionAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
