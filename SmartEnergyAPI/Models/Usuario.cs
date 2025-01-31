using System;
using System.Collections.Generic;

namespace SmartEnergyAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public string? TipoUsuario { get; set; }

    public virtual ICollection<Alerta> Alerta { get; set; } = new List<Alerta>();

    public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();

    public virtual ICollection<MetasAhorro> MetasAhorros { get; set; } = new List<MetasAhorro>();

    public virtual ICollection<Recomendacione> Recomendaciones { get; set; } = new List<Recomendacione>();

    public virtual ICollection<RegistroConsumo> RegistroConsumos { get; set; } = new List<RegistroConsumo>();
}
