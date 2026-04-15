using System.ComponentModel.DataAnnotations;

namespace examen.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Ingresa un correo")]
        [EmailAddress(ErrorMessage = "Ingresa un correo valido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa una contrasena")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contrasena debe tener al menos 6 caracteres")]
        [Display(Name = "Contrasena")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirma la contrasena")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contrasenas no coinciden")]
        [Display(Name = "Confirmar contrasena")]
        public string ConfirmarContrasena { get; set; } = string.Empty;
    }
}
