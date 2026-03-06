using examen.Base;
using examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace examen.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Categorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return View(categorias);
        }

        //CREAR CATEGORIA

        [HttpGet]
        public IActionResult CrearCategoria()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCategoria(Categoria categoria)
        {
            Console.WriteLine("Entró al POST");

            if (ModelState.IsValid)
            {

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Categorias));
            }
            return View(categoria);
        }


        //DETALLES DE CATEGORIA
        [HttpGet]
        public async Task<IActionResult> DetalleCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        //ELIMINAR CATEGORIA
        [HttpGet]
        public async Task<IActionResult> EliminarCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _context.Categorias.Include(c => c.Productos).FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            if (categoria.Productos.Any())
            {
                return RedirectToAction(nameof(Categorias));
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Categorias));
        }

        //ACTUALIZAR CATEGORIA

        [HttpGet]
        public async Task<IActionResult> EditarCategoria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCategoria(Categoria categoria)
        {

            if (ModelState.IsValid)
            {
                var categoriaDB = await _context.Categorias.FindAsync(categoria.Id);

                if (categoriaDB == null)
                {
                    return NotFound();
                }

                // Actualizar campos
                categoriaDB.Nombre = categoria.Nombre;
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Categorias));
            }

            return View(categoria);
        }




    }
}
