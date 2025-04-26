using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class EntrenadorEquipo
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? EquipoId { get; set; }

    public virtual Equipo? Equipo { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
