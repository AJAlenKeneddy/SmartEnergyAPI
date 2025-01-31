using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class Alerta
{
    public int AlertaId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaAlerta { get; set; }

    public string? TipoAlerta { get; set; }

    public string? Mensaje { get; set; }

    public bool? Resuelta { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
