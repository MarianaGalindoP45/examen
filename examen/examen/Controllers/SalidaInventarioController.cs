using examen.Base;
using examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace examen.Controllers
{
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

        //CREAR SALIDA DE INVENTARIO

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
                Console.WriteLine("ModelState válido");

                _context.SalidasInventario.Add(salida);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(SalidaInventario));
            }
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            Console.WriteLine("ModelState inválido");

            return View(salida);
        }

        //DETALLES DE SALIDA DE INVENTARIO
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
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");


            return View(salida);
        }

        //EDITAR SALIDA DE INVENTARIO

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
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
            return View(salida);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarSalida(SalidaInventario salida)
        {

            if (ModelState.IsValid)
            {

                var salidaDB = await _context.SalidasInventario.FindAsync(salida.Id);

                if (salidaDB == null)
                {
                    return NotFound();
                }

                // Actualizar campos
                salidaDB.ProductoId = salida.ProductoId;
                salidaDB.Cantidad = salida.Cantidad;
                salidaDB.Fecha = salida.Fecha;
                salidaDB.Motivo = salida.Motivo;

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(SalidaInventario));
            }

            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            Console.WriteLine("ModelState inválido");

            return View(salida);
        }



    }
}
