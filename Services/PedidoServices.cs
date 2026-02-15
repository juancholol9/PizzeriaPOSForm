using System.Text;
using Newtonsoft.Json;
using PosPizza.Controllers;
using PosPizza.Models;

namespace PosPizza.Services
{
    public class PedidoService
    {
        private readonly string _baseUrl = "http://localhost:5099";
        private readonly HttpClient _httpClient;

        public PedidoService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<PedidoDTO>> ObtenerTodos()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Pedido");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<PedidoDTO>>>(jsonResponse);
                    return result.Response ?? new List<PedidoDTO>();
                }

                throw new Exception($"Error al obtener pedidos: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener pedidos: {ex.Message}");
            }
        }

        public async Task<PedidoDetailDTO> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Pedido/{id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<PedidoDetailDTO>>(jsonResponse);
                    return result.Response;
                }

                throw new Exception($"Error al obtener pedido: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener pedido: {ex.Message}");
            }
        }

        public async Task<List<PedidoDTO>> ObtenerPorCliente(int clienteId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Pedido/cliente/{clienteId}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<PedidoDTO>>>(jsonResponse);
                    return result.Response ?? new List<PedidoDTO>();
                }

                throw new Exception($"Error al obtener pedidos del cliente: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener pedidos del cliente: {ex.Message}");
            }
        }

        public async Task<PedidoDetailDTO> Crear(PedidoCreateUpdateDTO pedido)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Pedido", content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<PedidoDetailDTO>>(jsonResponse);
                    return result.Response;
                }

                throw new Exception($"Error al crear pedido: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear pedido: {ex.Message}");
            }
        }

        public async Task<bool> Actualizar(int id, PedidoCreateUpdateDTO pedido)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Pedido/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar pedido: {ex.Message}");
            }
        }

        public async Task<bool> ActualizarEstado(int id, string estado)
        {
            try
            {
                var data = new { estado = estado };
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PatchAsync($"/api/Pedido/{id}/estado", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar estado del pedido: {ex.Message}");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Pedido/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar pedido: {ex.Message}");
            }
        }
    }


}