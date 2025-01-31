using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class VistaConsumoMensual
{
    public int UsuarioId { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Dispositivo { get; set; }

    public decimal? ConsumoUltimos30Dias { get; set; }
}
