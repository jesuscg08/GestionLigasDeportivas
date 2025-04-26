using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class Evento
{
    public int EventoId { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? Fecha { get; set; }

    public TimeOnly? Hora { get; set; }

    public int? EquipoLocalId { get; set; }

    public int? EquipoVisitanteId { get; set; }

    public bool? Notificacion { get; set; }

    public virtual Equipo? EquipoLocal { get; set; }

    public virtual Equipo? EquipoVisitante { get; set; }

    public virtual ICollection<Estadistica> Estadisticas { get; set; } = new List<Estadistica>();
}
