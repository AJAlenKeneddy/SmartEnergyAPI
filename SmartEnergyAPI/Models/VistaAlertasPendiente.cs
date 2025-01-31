using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class VistaAlertasPendiente
{
    public int UsuarioId { get; set; }

    public string? NombreUsuario { get; set; }

    public string? TipoAlerta { get; set; }

    public string? Mensaje { get; set; }

    public DateTime? FechaAlerta { get; set; }
}
