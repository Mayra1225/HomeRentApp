using HomeRentApp.Data;
using HomeRentApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace HomeRentApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HomeRentContextSQLServer _context;

        public UsuarioController(HomeRentContextSQLServer context)
        {
            _context = context;
        }


        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string usuarioId, string contraseña)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(u => u.UsuarioId == usuarioId && u.Contraseña == contraseña);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioId", usuario.UsuarioId);

                return RedirectToAction("Index", "Departamento");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }



        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
    }
}
