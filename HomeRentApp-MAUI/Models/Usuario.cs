using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeRentApp_MAUI.Models
{
    internal class Usuario
    {
        public string UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Correo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Contraseña { get; set; }
    }
}
