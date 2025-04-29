using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionLigasDeportivas.Models;

public partial class JugadorEquipo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
    public int? UsuarioId { get; set; }

    [Display(Name = "Equipo")]
    [Required(ErrorMessage = "Debes elegir el equipo")]
    public int? EquipoId { get; set; }

    public virtual Equipo? Equipo { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
