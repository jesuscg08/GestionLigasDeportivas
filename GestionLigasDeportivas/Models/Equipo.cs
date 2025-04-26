using System;
using System.Collections.Generic;

namespace GestionLigasDeportivas.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string? Nombre { get; set; }

    public int? LigaId { get; set; }

    public virtual ICollection<EntrenadorEquipo> EntrenadorEquipos { get; set; } = new List<EntrenadorEquipo>();

    public virtual ICollection<Estadistica> Estadisticas { get; set; } = new List<Estadistica>();

    public virtual ICollection<Evento> EventoEquipoLocals { get; set; } = new List<Evento>();

    public virtual ICollection<Evento> EventoEquipoVisitantes { get; set; } = new List<Evento>();

    public virtual ICollection<JugadorEquipo> JugadorEquipos { get; set; } = new List<JugadorEquipo>();

    public virtual Liga? Liga { get; set; }

    public virtual ICollection<PerfilEquipo> PerfilEquipos { get; set; } = new List<PerfilEquipo>();

    public virtual ICollection<PerfilJugador> PerfilJugadors { get; set; } = new List<PerfilJugador>();
}
