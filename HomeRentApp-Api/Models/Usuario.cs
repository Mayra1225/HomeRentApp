using System.ComponentModel.DataAnnotations;

namespace HomeRentApp_Api.Models
{
    public class Usuario
    {
        [Key]
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