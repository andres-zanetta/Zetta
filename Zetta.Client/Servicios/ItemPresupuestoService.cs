using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Zetta.Shared.DTOS.ItemPresupuesto;

namespace Zetta.Client.Servicios
{

    // NOTA: Asegúrate de registrar el servicio en Program.cs: builder.Services.AddScoped<ItemPresupuestoService>();

    public class ItemPresupuestoService : IItemPresupuestoService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/itempresupuesto"; // La ruta base de tu Web API

        public ItemPresupuestoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GET_ItemPresupuestoDTO>?> GetAll()
        {
            // GET devuelve una lista del DTO de lectura (GET_ItemPresupuestoDTO)
            return await _httpClient.GetFromJsonAsync<List<GET_ItemPresupuestoDTO>>(BaseUrl);
        }

        public async Task<GET_ItemPresupuestoDTO?> GetById(int id)
        {
            // GET por ID devuelve el DTO de lectura
            return await _httpClient.GetFromJsonAsync<GET_ItemPresupuestoDTO>($"{BaseUrl}/{id}");
        }

        public async Task Create(POST_ItemPresupuestoDTO item)
        {
            // POST recibe el DTO de creación
            await _httpClient.PostAsJsonAsync(BaseUrl, item);
        }

        public async Task Update(int id, PUT_ItemPresupuestoDTO item)
        {
            // PUT recibe el DTO de actualización
            await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", item);
        }

        public async Task Delete(int id)
        {
            // DELETE
            await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        }

        public async Task AplicarAumento(POST_AumentoMasivoDTO dto)
        {
            var resp = await _httpClient.PostAsJsonAsync($"{BaseUrl}/aumento-masivo", dto);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<List<GET_ItemPresupuestoDTO>?> GetInactivos()
        {
            return await _httpClient.GetFromJsonAsync<List<GET_ItemPresupuestoDTO>>($"{BaseUrl}/papelera");
        }

        public async Task Restaurar(int id)
        {
            var resp = await _httpClient.PutAsync($"{BaseUrl}/restaurar/{id}", null);
            resp.EnsureSuccessStatusCode();
        }
    }
}
