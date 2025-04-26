using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class Liga
{
    public int LigaId { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? NumeroJornadas { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<PerfilEquipo> PerfilEquipos { get; set; } = new List<PerfilEquipo>();
}
