using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.Server.Repositorios;
using Zetta.Shared.DTOS.Obra;

namespace Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/obra")]
    public class ObraController : ControllerBase
    {
        private readonly IObraRepositorio _obraRepositorio;
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ObraController(IObraRepositorio repositorio, Context context, IMapper mapper)
        {
            this._obraRepositorio = repositorio;
            this._context = context;
            this._mapper = mapper;
        }

        // GET: api/Obra
        [HttpGet]
        public async Task<ActionResult<List<GET_ObraDTO>>> Get()
        {
            try
            {
                var obras = await _obraRepositorio.ObtenerObrasConDetallesAsync();

                // Si la lista es null (raro pero posible), devolvemos lista vacía
                if (obras == null) return Ok(new List<GET_ObraDTO>());

                var obrasDTO = _mapper.Map<List<GET_ObraDTO>>(obras);
                return Ok(obrasDTO);
            }
            catch (Exception ex)
            {
                // Esto te mostrará el error en la consola del servidor (la pantalla negra de logs)
                Console.WriteLine($"ERROR CRÍTICO EN GET OBRA: {ex.Message}");
                return StatusCode(500, "Error interno al obtener obras: " + ex.Message);
            }
        }

        // GET: api/Obra/id
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GET_ObraDTO>> GetById(int id)
        {
            var obra = await _obraRepositorio.ObtenerObraPorIdConDetallesAsync(id);
            if (obra == null)
            {
                return NotFound("Obra no encontrada.");
            }
            var obraDTO = _mapper.Map<GET_ObraDTO>(obra);
            return Ok(obraDTO);
        }

        // POST: api/Obra
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] POST_ObraDTO obraDTO)
        {
            try
            {
                var obra = _mapper.Map<Obra>(obraDTO);
                var id = await _obraRepositorio.AddAsync(obra);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la obra: {ex.Message}");
            }
        }

        // PUT: api/Obra/id
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_ObraDTO obraDTO)
        {
            var obraExistente = await _context.Obras
                .Include(o => o.Comentarios)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (obraExistente == null) return NotFound("Obra no encontrada.");

            _mapper.Map(obraDTO, obraExistente);

            if (obraDTO.Comentarios != null && obraDTO.Comentarios.Any())
            {
                if (obraExistente.Comentarios == null) obraExistente.Comentarios = new List<Comentario>();

                var textosExistentes = obraExistente.Comentarios.Select(c => c.Texto).ToHashSet();

                foreach (var textoDto in obraDTO.Comentarios)
                {
                    if (!textosExistentes.Contains(textoDto))
                    {
                        obraExistente.Comentarios.Add(new Comentario
                        {
                            Texto = textoDto,
                            Fecha = DateTime.Now,
                            ObraId = id
                        });
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Obra actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la obra: {ex.Message}");
            }
        }

        // DELETE: api/Obra/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _obraRepositorio.DeleteAsync(id);
            return Ok("Obra eliminada correctamente.");
        }
    }
}
