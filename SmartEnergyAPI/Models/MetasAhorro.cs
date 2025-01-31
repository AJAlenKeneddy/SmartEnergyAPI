using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class MetasAhorro
{
    public int MetaId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public decimal? MetaConsumo { get; set; }

    public decimal? ConsumoActual { get; set; }

    public bool? Alcanzado { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
