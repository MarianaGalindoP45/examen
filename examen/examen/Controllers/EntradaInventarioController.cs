using examen.Base;
using examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace examen.Controllers
{
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
        //CREAR SALIDA DE INVENTARIO
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
            Console.WriteLine("Entró al POST");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState válido");

                _context.EntradasInventario.Add(entrada);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Producto creado exitosamente.";

                return RedirectToAction(nameof(EntradaInventario));
            }
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            Console.WriteLine("ModelState inválido");

            return View(entrada);
        }


        //DETALLES DE ENTRADA DE INVENTARIO
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
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");


            return View(entrada);
        }

        //EDITAR Entrada

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
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
            return View(entrada);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEntrada(EntradaInventario entrada)
        {
            Console.WriteLine("Entró al POST");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState válido");

                var entradaDB = await _context.EntradasInventario.FindAsync(entrada.Id);

                if (entradaDB == null)
                {
                    return NotFound();
                }

                // Actualizar campos
                entradaDB.ProductoId = entrada.ProductoId;
                entradaDB.Cantidad = entrada.Cantidad;
                entradaDB.Fecha = entrada.Fecha;
                entradaDB.Nota = entrada.Nota;

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(EntradaInventario));
            }

            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");

            Console.WriteLine("ModelState inválido");

            return View(entrada);
        }


    }
}
