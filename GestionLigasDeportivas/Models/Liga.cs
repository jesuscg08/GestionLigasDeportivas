using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionLigasDeportivas.Models;

public partial class Liga
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LigaId { get; set; }

    public string? Nombre { get; set; }

    [Display(Name = "Fecha de inicio")]
    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
    public DateOnly? FechaInicio { get; set; }

    [Display(Name = "Fecha de finalización")]
    [Required(ErrorMessage = "La fecha de finalización es obligatoria")]
    public DateOnly? FechaFin { get; set; }

    [Display(Name = "Números de jornadas")]
    [Required(ErrorMessage = "El número de jornadas es obligatoria")]
    public int? NumeroJornadas { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<PerfilEquipo> PerfilEquipos { get; set; } = new List<PerfilEquipo>();
}
