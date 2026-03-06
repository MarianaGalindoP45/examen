using examen.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using examen.ViewModels;

namespace examen.Controllers
{
    public class InventarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Stock(string buscar, int? categoriaId)
        {
            var productos = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.EntradasInventario)
                .Include(p => p.SalidasInventarios)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                productos = productos.Where(p => p.Nombre.Contains(buscar) || p.SKU.Contains(buscar));
            }

            if (categoriaId.HasValue)
            {
                productos = productos.Where(p => p.CategoriaId == categoriaId);
            }

            var resultado = await productos.Select(p => new StockViewModel
            {
                ProductoId = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria.Nombre,
                SKU = p.SKU,
                StockActual =
                    p.EntradasInventario.Sum(e => (int?)e.Cantidad) ?? 0
                    -
                    p.SalidasInventarios.Sum(s => (int?)s.Cantidad) ?? 0,
                StockMinimo = p.StockMinimo
            }).ToListAsync();

            return View(resultado);
        }
    }
}
