using examen.Base;
using examen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace examen.Controllers
{
    [Authorize]
    public class SalidaInventarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalidaInventarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SalidaInventario()
        {
            var salida = await _context.SalidasInventario.Include(e => e.Producto).ToListAsync();
            return View(salida);
        }

        [HttpGet]
        public IActionResult CrearSalida()
        {
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSalida(SalidaInventario salida)
        {
            if (ModelState.IsValid)
            {
                _context.SalidasInventario.Add(salida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SalidaInventario));
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(salida);
        }

        [HttpGet]
        public async Task<IActionResult> DetalleSalida(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salida = await _context.SalidasInventario.FirstOrDefaultAsync(m => m.Id == id);
            if (salida == null)
            {
                return NotFound();
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(salida);
        }

        [HttpGet]
        public async Task<IActionResult> EditarSalida(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salida = await _context.SalidasInventario.FindAsync(id);
            if (salida == null)
            {
                return NotFound();
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(salida);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarSalida(SalidaInventario salida)
        {
            if (ModelState.IsValid)
            {
                var salidaDb = await _context.SalidasInventario.FindAsync(salida.Id);
                if (salidaDb == null)
                {
                    return NotFound();
                }

                salidaDb.ProductoId = salida.ProductoId;
                salidaDb.Cantidad = salida.Cantidad;
                salidaDb.Fecha = salida.Fecha;
                salidaDb.Motivo = salida.Motivo;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SalidaInventario));
            }

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(salida);
        }
    }
}
