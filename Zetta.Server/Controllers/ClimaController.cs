using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zetta.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private const string ApiKey = "403747cd7730269d7c72d434130a461a";

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string ciudad = "Cordoba,AR")
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={ciudad}&appid={ApiKey}&units=metric&lang=es";

                using (var httpClient = new HttpClient())
                {
                    // El servidor hace la petición a OpenWeatherMap (Aquí NO hay CORS)
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        // Devolvemos el JSON crudo al cliente Blazor
                        return Ok(json);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error al obtener clima externo");
                    }
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}