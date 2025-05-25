using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeRentApp_MAUI.Models
{
    internal class Departamento
    {
        public int DepartamentoId { get; set; }


        public string? Imagen { get; set; } //Se quita el required para que se pueda subir como formato archivo normal

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Required]
        [Precision(18, 2)] //Sugerencia de gpt para mejorar la precision con 18 digitos y 2 decimales
        public decimal Precio { get; set; }

        [Required]
        public int CuartosDisponibles { get; set; }

        [Required]
        public string UsuarioId { get; set; } // Para saber qué usuario arrendador publicó el departamento
    }
}
