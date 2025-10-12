using System.Net.Http.Json;
using Zetta.BD.DATA.ENTITY;
using Zetta.Shared.DTOS.Presupuesto;
using Microsoft.AspNetCore.Components; // Si usas HttpClient de Blazor WASM/Server

namespace Zetta.Client.Servicios
{
    public class PresupuestoService:IPresupuestoServices
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "api/presupuestos"; // La ruta base de tu Web API
        public PresupuestoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GET_PresupuestoDTO>?> GetAll()
        {
            // GET devuelve una lista del DTO de lectura (GET_PresupuestoDTO)
            return await _httpClient.GetFromJsonAsync<List<GET_PresupuestoDTO>>(BaseUrl);
        }

        public async Task<GET_PresupuestoDTO?> GetById(int id)
        {
            // GET por ID devuelve el DTO de lectura
            return await _httpClient.GetFromJsonAsync<GET_PresupuestoDTO>($"{BaseUrl}/{id}");
        }

        public async Task<int> Create(POST_PresupuestoDTO presupuesto)
        {
            // POST recibe el DTO de creación
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, presupuesto);
            response.EnsureSuccessStatusCode();
            // Asumiendo que la API devuelve el ID del nuevo presupuesto en el cuerpo de la respuesta
            var createdId = await response.Content.ReadFromJsonAsync<int>();
            return createdId;
        }

        public async Task Update(int id, PUT_PresupuestoDTO presupuesto)
        {
            // PUT recibe el DTO de actualización
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", presupuesto);
            response.EnsureSuccessStatusCode();
        }
        public async Task Delete(int id)
        {
            // DELETE
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
