using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class PerfilEquipo
{
    public int PerfilEquipoId { get; set; }

    public int? EquipoId { get; set; }

    public int? LigaId { get; set; }

    public int? CantidadGoles { get; set; }

    public int? TarjetasAmarillas { get; set; }

    public int? TarjetasRojas { get; set; }

    public virtual Equipo? Equipo { get; set; }

    public virtual Liga? Liga { get; set; }
}
