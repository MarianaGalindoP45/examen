using System.ComponentModel.DataAnnotations;

namespace examen.Models
{
    public class SalidaInventario
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Es obligatorio seleccionar un producto")]
        public int ProductoId { get; set; }

        public Producto? Producto { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar una cantidad")]

        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Es obligatorio seleccionar una fecha")]

        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar un motivo")]

        public string ?Motivo { get; set; }
    }
}
