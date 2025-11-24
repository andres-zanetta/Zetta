using System.Net.Http.Json;
using Zetta.Shared.DTOS.VisitaTecnica;

namespace Zetta.Client.Servicios
{
    public class VisitaTecnicaService : IVisitaTecnicaService
    {
        private readonly HttpClient _httpClient;
        // Esta ruta debe coincidir con [Route("api/visitas")] en tu Controller
        private const string BaseUrl = "api/visitas";

        public VisitaTecnicaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GET_VisitaTecnicaDTO>?> GetAll()
        {
            try
            {
                // GET: api/visitas
                return await _httpClient.GetFromJsonAsync<List<GET_VisitaTecnicaDTO>>(BaseUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener visitas: {ex.Message}");
                return new List<GET_VisitaTecnicaDTO>();
            }
        }

        public async Task<GET_VisitaTecnicaDTO?> GetById(int id)
        {
            try
            {
                // GET: api/visitas/{id}
                return await _httpClient.GetFromJsonAsync<GET_VisitaTecnicaDTO>($"{BaseUrl}/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> Create(POST_VisitaTecnicaDTO visita)
        {
            // POST: api/visitas
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, visita);
            response.EnsureSuccessStatusCode();

            // Leemos el ID creado que devuelve el controlador
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task Update(int id, PUT_VisitaTecnicaDTO visita)
        {
            // PUT: api/visitas/{id}
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", visita);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            // DELETE: api/visitas/{id}
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
