using System.Text;
using Newtonsoft.Json;
using PosPizza.Controllers;
using PosPizza.Models;

namespace PosPizza.Services
{
    public class ClienteService
    {
        private readonly string _baseUrl = "http://localhost:5099";
        private readonly HttpClient _httpClient;

        public ClienteService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<ClienteDTO>> ObtenerTodos()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Cliente");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ClienteDTO>>>(jsonResponse);
                    return result.Response ?? new List<ClienteDTO>();
                }

                throw new Exception($"Error al obtener clientes: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener clientes: {ex.Message}");
            }
        }

        public async Task<ClienteDTO> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Cliente/{id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<ClienteDTO>>(jsonResponse);
                    return result.Response;
                }

                throw new Exception($"Error al obtener cliente: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cliente: {ex.Message}");
            }
        }

        public async Task<bool> Crear(ClienteCreateUpdateDTO cliente)
        {
            try
            {
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Cliente", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear cliente: {ex.Message}");
            }
        }

        public async Task<bool> Actualizar(int id, ClienteCreateUpdateDTO cliente)
        {
            try
            {
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Cliente/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar cliente: {ex.Message}");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Cliente/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar cliente: {ex.Message}");
            }
        }
    }

    
}