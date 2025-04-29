using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionLigasDeportivas.Models;

public partial class Evento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EventoId { get; set; }

    [Display(Name = "Nombre del evento o partido")]
    [Required(ErrorMessage = "El nombre del evento o partido es obligatoria")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateOnly? Fecha { get; set; }

    [Required(ErrorMessage = "La hora es obligatoria")]
    public TimeOnly? Hora { get; set; }

    [Display(Name = "Equipo local")]
    [Required(ErrorMessage = "Debes eligir un equipo")]
    public int? EquipoLocalId { get; set; }

    [Display(Name = "Equipo visitante")]
    [Required(ErrorMessage = "Debes eligir un equipo")]
    public int? EquipoVisitanteId { get; set; }

    public bool? Notificacion { get; set; }

    [Display(Name = "Equipo local")]
    public virtual Equipo? EquipoLocal { get; set; }

    [Display(Name = "Equipo visitante")]
    public virtual Equipo? EquipoVisitante { get; set; }

    public virtual ICollection<Estadistica> Estadisticas { get; set; } = new List<Estadistica>();
}
