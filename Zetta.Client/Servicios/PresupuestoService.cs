using System.Net.Http.Json;
using Zetta.Shared.DTOS.Presupuesto;

namespace Zetta.Client.Servicios
{
    public class PresupuestoService : IPresupuestoServices
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/presupuestos";

        public PresupuestoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GET_PresupuestoDTO>?> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<GET_PresupuestoDTO>>(BaseUrl);
        }

        public async Task<GET_PresupuestoDTO?> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<GET_PresupuestoDTO>($"{BaseUrl}/{id}");
        }

        public async Task<int> Create(POST_PresupuestoDTO presupuesto)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, presupuesto);
            response.EnsureSuccessStatusCode();
            var createdId = await response.Content.ReadFromJsonAsync<int>();
            return createdId;
        }

        public async Task Update(int id, PUT_PresupuestoDTO presupuesto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", presupuesto);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<GET_PresupuestoDTO>?> GetInactivos()
        {
            return await _httpClient.GetFromJsonAsync<List<GET_PresupuestoDTO>>($"{BaseUrl}/papelera");
        }

        public async Task Restaurar(int id)
        {
            var response = await _httpClient.PutAsync($"{BaseUrl}/restaurar/{id}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task EliminarDefinitivamente(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/definitivo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}