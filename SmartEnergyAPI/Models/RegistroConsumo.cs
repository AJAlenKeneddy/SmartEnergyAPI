using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class RegistroConsumo
{
    public int ConsumoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? DispositivoId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public decimal? ConsumoKwh { get; set; }

    public virtual Dispositivo? Dispositivo { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
