using System.Net.Http.Json;
using Zetta.Shared.DTOS.Cliente;
using System.Net.Http;
using Microsoft.AspNetCore.Components; // Si usas HttpClient de Blazor WASM/Server

namespace Zetta.Client.Servicios
{
    // NOTA: Asegúrate de registrar este servicio en Program.cs
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        // La ruta base debe coincidir con el controlador: [Route("/api/[controller]")] en ClienteController
        private const string BaseUrl = "/api/Cliente";

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GET_ClienteDTO>?> GetAll()
        {
            // Realiza una petición GET a /api/clientes
            return await _httpClient.GetFromJsonAsync<List<GET_ClienteDTO>>(BaseUrl);
        }

        public async Task<GET_ClienteDTO?> GetById(int id)
        {
            // Realiza una petición GET a /api/clientes/{id}
            return await _httpClient.GetFromJsonAsync<GET_ClienteDTO>($"{BaseUrl}/{id}");
        }

        public async Task Create(POST_ClienteDTO cliente)
        {
            // Realiza una petición POST a /api/clientes con el DTO de creación
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(int id, PUT_ClienteDTO cliente)
        {
            // Realiza una petición PUT a /api/clientes/{id} con el DTO de actualización
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            // Realiza una petición DELETE a /api/clientes/{id}
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}