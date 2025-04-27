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

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
    public string? Correo { get; set; }

    [Required(ErrorMessage = "Debe eligir un tipo de usuario")]
    public string? TipoUsuario { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatorio")]
    [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres", MinimumLength = 6)]
    [Display(Name = "Contraseña")]
    public string? Contrasena { get; set; }

    public string? TokenRecuperacion { get; set; }

    public virtual ICollection<AsistenciaEntrenamiento> AsistenciaEntrenamientos { get; set; } = new List<AsistenciaEntrenamiento>();

    public virtual ICollection<EntrenadorEquipo> EntrenadorEquipos { get; set; } = new List<EntrenadorEquipo>();

    public virtual ICollection<Estadistica> Estadisticas { get; set; } = new List<Estadistica>();

    public virtual ICollection<JugadorEquipo> JugadorEquipos { get; set; } = new List<JugadorEquipo>();

    public virtual ICollection<PerfilJugador> PerfilJugadors { get; set; } = new List<PerfilJugador>();
}
