using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeRentApp_MAUI.Models
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }

        public string? Imagen { get; set; }

        public string? ImagenUrl { get; set; } //Se quita el required para que se pueda subir como formato archivo normal

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public decimal Precio { get; set; }

        public int CuartosDisponibles { get; set; }

        public string UsuarioId { get; set; } // Para saber qué usuario arrendador publicó el departamento
    }
}
