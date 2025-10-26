using System.Net.Http.Json;
using Zetta.Shared.DTOS.Obra;

namespace Zetta.Client.Servicios
{
    public class ObraService : IObraService
    {
        private readonly HttpClient _http;

        public ObraService(HttpClient http)
        {
            _http = http;
        }

        // URL base consistente para el API de obras
        private const string BaseUrl = "api/obra";

        public async Task<List<GET_ObraDTO>> GetAllAsync()
        {
            var resp = await _http.GetFromJsonAsync<List<GET_ObraDTO>>(BaseUrl);
            return resp ?? new List<GET_ObraDTO>();
        }

        public async Task<GET_ObraDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<GET_ObraDTO>($"{BaseUrl}/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> CreateAsync(POST_ObraDTO model)
        {
            var resp = await _http.PostAsJsonAsync(BaseUrl, model);

            if (resp.IsSuccessStatusCode)
            {
                // 1) Intentar leer un int simple en el body (p. ej. API devuelve solo el id)
                try
                {
                    var idFromBody = await resp.Content.ReadFromJsonAsync<int?>();
                    if (idFromBody.HasValue)
                        return idFromBody.Value;
                }
                catch
                {
                    // ignored - intentaremos otras estrategias
                }

                // 2) Intentar leer el objeto creado y devolver su Id
                try
                {
                    var createdObj = await resp.Content.ReadFromJsonAsync<GET_ObraDTO?>();
                    if (createdObj != null)
                        return createdObj.Id;
                }
                catch
                {
                    // ignored
                }

                // 3) Intentar extraer id desde la cabecera Location
                if (resp.Headers.Location != null)
                {
                    var segments = resp.Headers.Location.Segments;
                    if (segments.Length > 0)
                    {
                        var last = segments[^1].Trim('/');
                        if (int.TryParse(last, out var idFromLocation))
                            return idFromLocation;
                    }
                }

                // 4) No se pudo determinar el id creado: devolver 0 (o cambiar por excepción si se prefiere)
                return 0;
            }

            var message = await resp.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error creando obra: {(int)resp.StatusCode} - {message}");
        }

        public async Task UpdateAsync(int id, PUT_ObraDTO model)
        {
            var resp = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", model);
            if (!resp.IsSuccessStatusCode)
            {
                var message = await resp.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error actualizando obra {id}: {(int)resp.StatusCode} - {message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"{BaseUrl}/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                var message = await resp.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error eliminando obra {id}: {(int)resp.StatusCode} - {message}");
            }
        }
    }
}