using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HomeRentApp_MAUI.Models;

namespace HomeRentApp_MAUI.Services
{
    public class DepartamentoService
    {
        private HttpClient _httpClient = new HttpClient();
        private const string baseUrl = "departamentos"; // Reemplaza con tu URL real

        public DepartamentoService()
        {
            _httpClient = new HttpClient();
#if ANDROID
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:5189/api/");
#else
        _httpClient.BaseAddress = new Uri("http://localhost:5189/api/");
#endif
        }

        public async Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            var response = await _httpClient.GetAsync(baseUrl);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al obtener departamentos");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Departamento>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        public async Task CrearDepartamentoMultipartAsync(MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(baseUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al crear: {error}");
            }
        }

        public async Task ActualizarDepartamentoMultipartAsync(int id, MultipartFormDataContent content)
        {
            var response = await _httpClient.PutAsync($"{baseUrl}/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar: {error}");
            }
        }


        public async Task EliminarDepartamentoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar: {error}");
            }
        }


    }
}
