using System.ComponentModel.DataAnnotations;

namespace examen.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingresa un correo")]
        [EmailAddress(ErrorMessage = "Ingresa un correo valido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingresa una contrasena")]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasena")]
        public string Contrasena { get; set; } = string.Empty;
    }
}
