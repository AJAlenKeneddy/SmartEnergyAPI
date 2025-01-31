using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class Dispositivo
{
    public int DispositivoId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaInstalacion { get; set; }

    public virtual ICollection<RegistroConsumo> RegistroConsumos { get; set; } = new List<RegistroConsumo>();

    public virtual Usuario? Usuario { get; set; }
}
