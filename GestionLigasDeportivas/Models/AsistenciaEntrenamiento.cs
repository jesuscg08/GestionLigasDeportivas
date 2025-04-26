using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class AsistenciaEntrenamiento
{
    public int AsistenciaId { get; set; }

    public int? EntrenamientoId { get; set; }

    public int? UsuarioId { get; set; }

    public string? EstadoAsistencia { get; set; }

    public virtual Entrenamiento? Entrenamiento { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
