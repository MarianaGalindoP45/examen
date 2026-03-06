using System.ComponentModel.DataAnnotations;

namespace examen.Models
{
    public class EntradaInventario
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Es obligatorio seleccionar un producto")]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

        [Required(ErrorMessage ="La cantidad es obligatoria")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage ="La Fecha es obligatoria")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage ="Es obligatorio añadir una nota")]
        public string ?Nota { get; set; }


    }
}
