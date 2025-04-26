﻿using System.ComponentModel.DataAnnotations;

namespace GestionLigasDeportivas.Models
{
    public class RecuperarContrasena
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }
    }

    public class NuevaClaveViewModel
    {
        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NuevaContrasena { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("NuevaClave", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena
        {
            get; set;
        }
    }

    }