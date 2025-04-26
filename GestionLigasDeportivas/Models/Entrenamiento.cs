using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class Entrenamiento
{
    public int EntrenamientoId { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Detalle { get; set; }

    public bool? Notificacion { get; set; }

    public virtual ICollection<AsistenciaEntrenamiento> AsistenciaEntrenamientos { get; set; } = new List<AsistenciaEntrenamiento>();
}
