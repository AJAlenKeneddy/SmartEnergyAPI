using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class Recomendacione
{
    public int RecomendacionId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime? FechaRecomendacion { get; set; }

    public string? TipoRecomendacion { get; set; }

    public string? Descripcion { get; set; }

    public string? AccionSugerida { get; set; }

    public virtual Usuario? Usuario { get; set; }
}   
