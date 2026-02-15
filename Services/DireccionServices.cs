using System.Text;
using Newtonsoft.Json;
using PosPizza.Controllers;
using PosPizza.Models;

namespace PosPizza.Services
{
    public class DireccionService
    {
        private readonly string _baseUrl = "http://localhost:5099";
        private readonly HttpClient _httpClient;

        public DireccionService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<DireccionDTO>> ObtenerTodas()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Direccion");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<DireccionDTO>>>(jsonResponse);
                    return result.Response ?? new List<DireccionDTO>();
                }

                throw new Exception($"Error al obtener direcciones: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener direcciones: {ex.Message}");
            }
        }

        public async Task<DireccionDTO> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Direccion/{id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<DireccionDTO>>(jsonResponse);
                    return result.Response;
                }

                throw new Exception($"Error al obtener dirección: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener dirección: {ex.Message}");
            }
        }

        public async Task<List<DireccionDTO>> ObtenerPorCliente(int clienteId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Direccion/cliente/{clienteId}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<DireccionDTO>>>(jsonResponse);
                    return result.Response ?? new List<DireccionDTO>();
                }

                throw new Exception($"Error al obtener direcciones del cliente: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener direcciones del cliente: {ex.Message}");
            }
        }

        public async Task<bool> Crear(DireccionCreateUpdateDTO direccion)
        {
            try
            {
                var json = JsonConvert.SerializeObject(direccion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Direccion", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear dirección: {ex.Message}");
            }
        }

        public async Task<bool> Actualizar(int id, DireccionCreateUpdateDTO direccion)
        {
            try
            {
                var json = JsonConvert.SerializeObject(direccion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Direccion/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar dirección: {ex.Message}");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Direccion/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar dirección: {ex.Message}");
            }
        }
    }


}