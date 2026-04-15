using System.ComponentModel.DataAnnotations;

namespace examen.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingresa un correo")]
        [EmailAddress(ErrorMessage = "Ingresa un correo valido")]
        [StringLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string ContrasenaHash { get; set; } = string.Empty;
    }
}
