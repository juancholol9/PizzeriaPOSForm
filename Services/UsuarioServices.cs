using System.Text;
using Newtonsoft.Json;
using PosPizza.Controllers;
using PosPizza.Models;

namespace PosPizza.Services
{
    public class UsuarioService
    {
        private readonly string _baseUrl = "http://localhost:5099";
        private readonly HttpClient _httpClient;

        public UsuarioService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<UsuarioDTO>> ObtenerTodos()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Usuario");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<UsuarioDTO>>>(jsonResponse);
                    return result.Response ?? new List<UsuarioDTO>();
                }

                throw new Exception($"Error al obtener usuarios: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuarios: {ex.Message}");
            }
        }

        public async Task<UsuarioDTO> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Usuario/{id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<UsuarioDTO>>(jsonResponse);
                    return result.Response;
                }

                throw new Exception($"Error al obtener usuario: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener usuario: {ex.Message}");
            }
        }

        public async Task<bool> Crear(UsuarioCreateUpdateDTO usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Usuario", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}");
            }
        }

        public async Task<bool> Actualizar(int id, UsuarioCreateUpdateDTO usuario)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Usuario/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar usuario: {ex.Message}");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Usuario/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar usuario: {ex.Message}");
            }
        }
    }
}