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
            var departamentos = await _context.Departamento.ToListAsync();

            var resultado = departamentos.Select(d => new DepartamentoResponseDto
            {
                DepartamentoId = d.DepartamentoId,
                ImagenUrl = $"{Request.Scheme}://{Request.Host}/uploads/{d.Imagen}",
                Nombre = d.Nombre,
                Direccion = d.Direccion,
                Precio = d.Precio,
                CuartosDisponibles = d.CuartosDisponibles,
                UsuarioId = d.UsuarioId
            });

            return Ok(resultado);
        }

        // Replace the return statement in the GetDepartamento method to ensure the correct ActionResult type is returned.

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoResponseDto>> GetDepartamento(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
                return NotFound();

            var dto = new DepartamentoResponseDto
            {
                DepartamentoId = departamento.DepartamentoId,
                ImagenUrl = $"{Request.Scheme}://{Request.Host}/uploads/{departamento.Imagen}",
                Nombre = departamento.Nombre,
                Direccion = departamento.Direccion,
                Precio = departamento.Precio,
                CuartosDisponibles = departamento.CuartosDisponibles,
                UsuarioId = departamento.UsuarioId
            };

            return Ok(dto); // Use Ok() to wrap the DTO in an ActionResult.
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

            var response = new DepartamentoResponseDto
            {
                DepartamentoId = departamento.DepartamentoId,
                ImagenUrl = $"{Request.Scheme}://{Request.Host}/uploads/{departamento.Imagen}",
                Nombre = departamento.Nombre,
                Direccion = departamento.Direccion,
                Precio = departamento.Precio,
                CuartosDisponibles = departamento.CuartosDisponibles,
                UsuarioId = departamento.UsuarioId
            };

            return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.DepartamentoId }, response);
        }


        // PUT: api/Departamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, [FromForm] DepartamentoEditarDto dto)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
                return NotFound();

            if (dto.Nombre != null) departamento.Nombre = dto.Nombre;
            if (dto.Direccion != null) departamento.Direccion = dto.Direccion;
            if (dto.Precio != null) departamento.Precio = dto.Precio.Value;
            if (dto.CuartosDisponibles != null) departamento.CuartosDisponibles = dto.CuartosDisponibles.Value;
            if (dto.UsuarioId != null) departamento.UsuarioId = dto.UsuarioId;


            if (dto.Imagen != null && dto.Imagen.Length > 0)
            {
                // Ruta del archivo anterior
                var carpeta = Path.Combine("wwwroot", "uploads");
                var imagenAnterior = Path.Combine(carpeta, departamento.Imagen);

                // Eliminar imagen anterior si existe
                if (!string.IsNullOrEmpty(departamento.Imagen) && System.IO.File.Exists(imagenAnterior))
                {
                    System.IO.File.Delete(imagenAnterior);
                }

                // Guardar nueva imagen
                var nuevoNombre = Guid.NewGuid().ToString() + Path.GetExtension(dto.Imagen.FileName);
                var nuevaRuta = Path.Combine(carpeta, nuevoNombre);

                using (var stream = new FileStream(nuevaRuta, FileMode.Create))
                {
                    await dto.Imagen.CopyToAsync(stream);
                }

                departamento.Imagen = nuevoNombre;
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

            // Ruta del archivo
            var ruta = Path.Combine("wwwroot", "uploads", departamento.Imagen);

            // Eliminar imagen si existe
            if (!string.IsNullOrEmpty(departamento.Imagen) && System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
