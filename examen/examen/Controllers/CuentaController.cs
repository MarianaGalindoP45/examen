using examen.Base;
using examen.Models;
using examen.Services;
using examen.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace examen.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public CuentaController(
            ApplicationDbContext context,
            IPasswordHasher<Usuario> passwordHasher,
            IJwtTokenService jwtTokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registro(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new RegistroViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var correo = model.Correo.Trim();
            var usuarioExistente = await _context.Usuarios.AnyAsync(u => u.Correo == correo);
            if (usuarioExistente)
            {
                ModelState.AddModelError(nameof(model.Correo), "Ese correo ya esta registrado.");
                return View(model);
            }

            var usuario = new Usuario
            {
                Correo = correo
            };

            usuario.ContrasenaHash = _passwordHasher.HashPassword(usuario, model.Contrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var token = _jwtTokenService.GenerateToken(usuario);
            Response.Cookies.Append("auth_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            TempData["SuccessMessage"] = "Cuenta creada correctamente.";
            return RedirectToLocal(returnUrl);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToLocal(returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Correo.Trim());
            if (usuario is null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales invalidas.");
                return View(model);
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.ContrasenaHash, model.Contrasena);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Credenciales invalidas.");
                return View(model);
            }

            var token = _jwtTokenService.GenerateToken(usuario);
            Response.Cookies.Append("auth_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            TempData["SuccessMessage"] = "Sesion iniciada correctamente.";
            return RedirectToLocal(returnUrl);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth_token");
            TempData["SuccessMessage"] = "Sesion cerrada correctamente.";
            return RedirectToAction(nameof(Login));
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Stock", "Inventario");
        }
    }
}
