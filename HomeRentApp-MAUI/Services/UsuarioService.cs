using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HomeRentApp_MAUI.Models;

namespace HomeRentApp_MAUI.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "Usuarios"; // Reemplaza con tu URL real


        public UsuarioService()
        {
            _httpClient = new HttpClient();
#if ANDROID
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:5189/api/");
#else
        _httpClient.BaseAddress = new Uri("http://localhost:5189/api/");
#endif
        }

        public async Task RegistrarUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("usuarios", usuario);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al registrar: {error}");
            }
        }

        public async Task<Usuario?> AutenticarUsuarioAsync(string correo, string contrasena)
        {
            var usuarios = await _httpClient.GetFromJsonAsync<List<Usuario>>("usuarios");
            return usuarios?.FirstOrDefault(u => u.Correo == correo && u.Contraseña == contrasena);
        }
    }
}
