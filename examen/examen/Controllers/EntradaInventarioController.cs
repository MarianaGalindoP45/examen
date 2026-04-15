using examen.Base;
using examen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace examen.Controllers
{
    [Authorize]
    public class EntradaInventarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntradaInventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> EntradaInventario()
        {
            var entradas = await _context.EntradasInventario.Include(e => e.Producto).ToListAsync();
            return View(entradas);
        }

        [HttpGet]
        public IActionResult CrearEntrada()
        {
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEntrada(EntradaInventario entrada)
        {
            if (ModelState.IsValid)
            {
                _context.EntradasInventario.Add(entrada);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Entrada creada exitosamente.";
                return RedirectToAction(nameof(EntradaInventario));
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(entrada);
        }

        [HttpGet]
        public async Task<IActionResult> DetalleEntrada(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.EntradasInventario.FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(entrada);
        }

        [HttpGet]
        public async Task<IActionResult> EditarEntrada(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.EntradasInventario.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(entrada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEntrada(EntradaInventario entrada)
        {
            if (ModelState.IsValid)
            {
                var entradaDb = await _context.EntradasInventario.FindAsync(entrada.Id);
                if (entradaDb == null)
                {
                    return NotFound();
                }

                entradaDb.ProductoId = entrada.ProductoId;
                entradaDb.Cantidad = entrada.Cantidad;
                entradaDb.Fecha = entrada.Fecha;
                entradaDb.Nota = entrada.Nota;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EntradaInventario));
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(entrada);
        }
    }
}
