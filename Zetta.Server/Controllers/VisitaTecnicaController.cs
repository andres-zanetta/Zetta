using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.Server.Repositorios;
using Zetta.Shared.DTOS.VisitaTecnica;


namespace Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/visitas")]
    public class VisitaTecnicaController : ControllerBase
    {
        private readonly IVisitaTecnicaRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly Context _context;

        public VisitaTecnicaController(IVisitaTecnicaRepositorio repositorio, IMapper mapper, Context context)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GET_VisitaTecnicaDTO>>> GetAll()
        {
            var visitas = await _repositorio.GetAllConDetallesAsync();
            var dtos = _mapper.Map<List<GET_VisitaTecnicaDTO>>(visitas);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GET_VisitaTecnicaDTO>> GetById(int id)
        {
            var visita = await _repositorio.GetByIdAsync(id);
            if (visita == null) return NotFound("Visita no encontrada");
            return Ok(_mapper.Map<GET_VisitaTecnicaDTO>(visita));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(POST_VisitaTecnicaDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<VisitaTecnica>(dto);
                // Estado inicial siempre Pendiente al crear
                entidad.Estado = EstadoVisita.Pendiente;

                await _repositorio.AddAsync(entidad);
                // El repositorio genérico ya hace SaveChanges, pero si no:
                // await _context.SaveChangesAsync(); 

                return Ok(entidad.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear visita: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PUT_VisitaTecnicaDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID no coincide");

            var entidad = await _repositorio.GetByIdAsync(id);
            if (entidad == null) return NotFound("Visita no encontrada");

            _mapper.Map(dto, entidad);

            try
            {
                await _repositorio.UpdateAsync(entidad);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entidad = await _repositorio.GetByIdAsync(id);
            if (entidad == null) return NotFound();

            await _repositorio.DeleteAsync(id);
            return NoContent();
        }
    }
}
