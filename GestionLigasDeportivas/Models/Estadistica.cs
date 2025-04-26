using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class Estadistica
{
    public int EstadisticaId { get; set; }

    public int? EventoId { get; set; }

    public int? JugadorId { get; set; }

    public int? EquipoId { get; set; }

    public int? Goles { get; set; }

    public int? Asistencias { get; set; }

    public int? TarjetasRojas { get; set; }

    public int? TarjetasAmarillas { get; set; }

    public virtual Equipo? Equipo { get; set; }

    public virtual Evento? Evento { get; set; }

    public virtual Usuario? Jugador { get; set; }
}
