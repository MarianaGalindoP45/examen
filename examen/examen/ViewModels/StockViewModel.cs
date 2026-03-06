namespace examen.ViewModels
{
    public class StockViewModel
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string SKU { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
    }
}
