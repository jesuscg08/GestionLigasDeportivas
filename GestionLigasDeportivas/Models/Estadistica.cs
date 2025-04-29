using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionLigasDeportivas.Models;

public partial class Estadistica
{

    public int EstadisticaId { get; set; }

    [Display(Name = "Nombre del evento")]
    [Required(ErrorMessage = "Debes escoger un evento")]
    public int? EventoId { get; set; }

    [Display(Name = "Nombre del jugador")]
    [Required(ErrorMessage = "Debes escoger un jugador")]
    public int? JugadorId { get; set; }

    [Display(Name = "Nombre del equipo")]
    [Required(ErrorMessage = "Debes escoger un eaquipo")]
    public int? EquipoId { get; set; }

    [Required(ErrorMessage = "Debes ingresar los goles")]
    public int? Goles { get; set; }

    [Required(ErrorMessage = "Debes ingresar las asistencias")]
    public int? Asistencias { get; set; }

    [Display(Name = "Tarjetas rojas")]
    [Required(ErrorMessage = "Debes ingresar las tarjeta rojas")]
    public int? TarjetasRojas { get; set; }

    [Display(Name = "Tarjetas Amarillas")]
    [Required(ErrorMessage = "Debes ingresar las tarjeta amarillas")]
    public int? TarjetasAmarillas { get; set; }

    public virtual Equipo? Equipo { get; set; }

    public virtual Evento? Evento { get; set; }

    public virtual Usuario? Jugador { get; set; }
}
