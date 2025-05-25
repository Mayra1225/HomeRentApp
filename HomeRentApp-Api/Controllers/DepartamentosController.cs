using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeRentApp_Api.Data;
using HomeRentApp_Api.Models;

namespace HomeRentApp_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly HomeRentApp_ApiContext _context;

        public DepartamentosController(HomeRentApp_ApiContext context)
        {
            _context = context;
        }

        // GET: api/Departamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
            return await _context.Departamento.ToListAsync();
        }

        // GET: api/Departamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
                return NotFound();

            return departamento;
        }

        // POST: api/Departamentos
        [HttpPost]
        [RequestSizeLimit(10_000_000)] // Máx 10MB, puedes ajustar
        public async Task<ActionResult<Departamento>> PostDepartamento([FromForm] DepartamentoDto dto)
        {
            var departamento = new Departamento
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Precio = dto.Precio,
                CuartosDisponibles = dto.CuartosDisponibles,
                UsuarioId = dto.UsuarioId
            };

            if (dto.Imagen != null && dto.Imagen.Length > 0)
            {
                string folder = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Imagen.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Imagen.CopyToAsync(stream);
                }

                departamento.Imagen = fileName;
            }

            _context.Departamento.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.DepartamentoId }, departamento);
        }


        // PUT: api/Departamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, [FromForm] DepartamentoDto dto)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null) return NotFound();

            departamento.Nombre = dto.Nombre;
            departamento.Direccion = dto.Direccion;
            departamento.Precio = dto.Precio;
            departamento.CuartosDisponibles = dto.CuartosDisponibles;
            departamento.UsuarioId = dto.UsuarioId;

            if (dto.Imagen != null && dto.Imagen.Length > 0)
            {
                string folder = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Imagen.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Imagen.CopyToAsync(stream);
                }

                departamento.Imagen = fileName;
            }

            _context.Entry(departamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Departamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
                return NotFound();

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.DepartamentoId == id);
        }
    }
}
