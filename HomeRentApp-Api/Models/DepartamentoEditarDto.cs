namespace HomeRentApp_Api.Models
{
    public class DepartamentoEditarDto
    {
        public IFormFile? Imagen { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public decimal? Precio { get; set; }
        public int? CuartosDisponibles { get; set; }
        public string? UsuarioId { get; set; }
    }
}
