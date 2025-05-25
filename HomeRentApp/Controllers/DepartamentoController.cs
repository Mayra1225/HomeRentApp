using HomeRentApp.Data;
using HomeRentApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HomeRentApp.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly HomeRentContextSQLServer _context;

        public DepartamentoController(HomeRentContextSQLServer context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departamentos = _context.Departamento.ToList();
            return View(departamentos);
        }

        public IActionResult Agregar()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Departamento departamento, IFormFile ImagenArchivo)
        {
            //Para este metodo recibi ayuda de ia para poder integrar la funcionalidad de agregar imagen y convertirla a base 64
            if (ModelState.IsValid)
            {
                var usuarioId = HttpContext.Session.GetString("UsuarioId");

                if (string.IsNullOrEmpty(usuarioId))
                {
                    return RedirectToAction("Login", "Usuario");
                }

                departamento.UsuarioId = usuarioId;

                if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ImagenArchivo.CopyTo(memoryStream);
                        byte[] imagenBytes = memoryStream.ToArray();
                        departamento.Imagen = Convert.ToBase64String(imagenBytes);
                    }
                }

                _context.Departamento.Add(departamento);
                _context.SaveChanges();

                return RedirectToAction("Index", "Departamento");
            }

            return View(departamento);
        }










    }
}
