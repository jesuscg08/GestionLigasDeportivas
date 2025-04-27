using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionLigasDeportivas.Models;

public partial class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? TipoUsuario { get; set; }

    public string? Contrasena { get; set; }

    public string? TokenRecuperacion { get; set; }

    public virtual ICollection<AsistenciaEntrenamiento> AsistenciaEntrenamientos { get; set; } = new List<AsistenciaEntrenamiento>();

    public virtual ICollection<EntrenadorEquipo> EntrenadorEquipos { get; set; } = new List<EntrenadorEquipo>();

    public virtual ICollection<Estadistica> Estadisticas { get; set; } = new List<Estadistica>();

    public virtual ICollection<JugadorEquipo> JugadorEquipos { get; set; } = new List<JugadorEquipo>();

    public virtual ICollection<PerfilJugador> PerfilJugadors { get; set; } = new List<PerfilJugador>();
}
