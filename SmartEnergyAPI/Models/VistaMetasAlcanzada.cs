using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class VistaMetasAlcanzada
{
    public int UsuarioId { get; set; }

    public string? NombreUsuario { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public decimal? MetaConsumo { get; set; }

    public decimal? ConsumoActual { get; set; }

    public string EstadoMeta { get; set; } = null!;
}
