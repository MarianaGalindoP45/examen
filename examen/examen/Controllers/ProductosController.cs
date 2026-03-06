using examen.Base;
using examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace examen.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Productos()
        {
            
            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();       
            return View(productos);

        }

        //CREAR PRODUCTO

        [HttpGet]
        public IActionResult CrearProducto()
        {
            var categorias = _context.Categorias.ToList();

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            Console.WriteLine("Entró al POST");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState válido");

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Producto creado exitosamente.";

                return RedirectToAction(nameof(Productos));
            }

            Console.WriteLine("ModelState inválido");

            return View(producto);
        }

        //EDITAR PRODUCTO

        [HttpGet]
        public async Task<IActionResult> EditarProducto(int? id)
        {
          if(id == null)
            {
                return NotFound();
            }
          var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            var categorias = await _context.Categorias.ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProducto(Producto producto)
        {
            Console.WriteLine("Entró al POST");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState válido");

                var productoDB = await _context.Productos.FindAsync(producto.Id);

                if (productoDB == null)
                {
                    return NotFound();
                }

                // Actualizar campos
                productoDB.Nombre = producto.Nombre;
                productoDB.SKU = producto.SKU;
                productoDB.Descripcion = producto.Descripcion;
                productoDB.Precio = producto.Precio;
                productoDB.UnidadMedida = producto.UnidadMedida;
                productoDB.CategoriaId = producto.CategoriaId;
                productoDB.StockMinimo = producto.StockMinimo;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Producto actualizado exitosamente.";

                return RedirectToAction(nameof(Productos));
            }

            var categorias = await _context.Categorias.ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaId);

            Console.WriteLine("ModelState inválido");

            return View(producto);
        }

        //DETALLES DEL PRODUCTO
        [HttpGet]
        public async Task<IActionResult> DetalleProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            var categorias = await _context.Categorias.ToListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", producto.CategoriaId);


            return View(producto);
        }



        //ELIMINAR PRODUCTO
        [HttpGet]
        public async Task<IActionResult> EliminarProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto != null) {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Producto eliminado exitosamente.";
                return RedirectToAction(nameof(Productos));
            }

            return View(producto);
        }


    }

}
