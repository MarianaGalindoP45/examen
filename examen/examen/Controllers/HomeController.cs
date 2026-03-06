using System.Diagnostics;
using examen.Models;
using Microsoft.AspNetCore.Mvc;

namespace examen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Categorias()
        {
            return View();
        }
        public IActionResult Productos()
        {
            return View();
        }
        public IActionResult EntradaInventario()
        {
            return View();
        }

        public IActionResult SalidaInventario()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
