using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace examen.Models
{
   

    [Index(nameof(SKU), IsUnique = true)]
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string ?Nombre { get; set; }

        [Required(ErrorMessage = "El SKU es obligatorio")]
        [StringLength(18, ErrorMessage = "El SKU no puede tener más de 18 caracteres")]
        public string SKU { get; set; }
        [Required(ErrorMessage = "Es obligatorio")]

        public string ?Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Precision(18, 2)]

        public decimal Precio { get; set; }

        public string ?UnidadMedida { get; set; }

        public int StockMinimo { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public ICollection<EntradaInventario> EntradasInventario { get; set; } = new List<EntradaInventario>();

        public ICollection<SalidaInventario> SalidasInventarios { get; set; } = new List<SalidaInventario>();

    }
}
