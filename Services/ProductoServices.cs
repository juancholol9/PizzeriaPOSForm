using System.Text;
using Newtonsoft.Json;
using PosPizza.Controllers;
using PosPizza.Models;

namespace PosPizza.Services
{
    public class ProductoService
    {
        private readonly string _baseUrl = "http://localhost:5099";
        private readonly HttpClient _httpClient;

        public ProductoService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<List<ProductoDTO>> ObtenerTodos()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Producto");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ProductoDTO>>>(jsonResponse);
                    return result.Response ?? new List<ProductoDTO>();
                }

                throw new Exception($"Error al obtener productos: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos: {ex.Message}");
            }
        }

        public async Task<List<CategoriaDTO>> ObtenerCategoria()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Categoria");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<CategoriaDTO>>>(jsonResponse);
                    return result.Response ?? new List<CategoriaDTO>();
                }

                throw new Exception($"Error al obtener categorias: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener categorias: {ex.Message}");
            }
        }

        public async Task<ProductoDTO> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Producto/{id}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<ProductoDTO>>(jsonResponse);
                    return result.Response;
                }

                throw new Exception($"Error al obtener producto: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener producto: {ex.Message}");
            }
        }

        public async Task<List<ProductoDTO>> ObtenerPorCategoria(int categoriaId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Producto/categoria/{categoriaId}");
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<ProductoDTO>>>(jsonResponse);
                    return result.Response ?? new List<ProductoDTO>();
                }

                throw new Exception($"Error al obtener productos por categoría: {jsonResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener productos por categoría: {ex.Message}");
            }
        }

        public async Task<bool> Crear(ProductoCreateUpdateDTO producto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Producto", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear producto: {ex.Message}");
            }
        }

        public async Task<bool> Actualizar(int id, ProductoCreateUpdateDTO producto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Producto/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar producto: {ex.Message}");
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Producto/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar producto: {ex.Message}");
            }
        }
    }


}