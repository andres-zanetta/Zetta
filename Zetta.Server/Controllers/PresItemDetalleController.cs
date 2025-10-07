using Microsoft.AspNetCore.Mvc;
using Zetta.Server.Repositorios;
using Zetta.Shared.DTOS.PresItemDetalle;
using Zetta.BD.DATA.ENTITY;
using AutoMapper;

namespace Zetta.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresItemDetalleController : ControllerBase
    {
        private readonly IPresItemDetalleRepositorio _repositorio;
        private readonly IMapper _mapper;

        public PresItemDetalleController(IPresItemDetalleRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        // GET: api/PresItemDetalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GET_PresItemDetalleDTO>>> GetAll()
        {
            var detalles = await _repositorio.SelectAllAsync();
            return Ok(_mapper.Map<IEnumerable<GET_PresItemDetalleDTO>>(detalles));
        }

        // GET: api/PresItemDetalle/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GET_PresItemDetalleDTO>> GetById(int id)
        {
            var detalle = await _repositorio.GetByIdAsync(id);
            if (detalle == null) return NotFound();
            return Ok(_mapper.Map<GET_PresItemDetalleDTO>(detalle));
        }

        // GET: api/PresItemDetalle/presupuesto/5
        [HttpGet("presupuesto/{presupuestoId}")]
        public async Task<ActionResult<IEnumerable<GET_PresItemDetalleDTO>>> GetByPresupuesto(int presupuestoId)
        {
            var detalles = await _repositorio.GetByPresupuestoIdAsync(presupuestoId);
            return Ok(_mapper.Map<IEnumerable<GET_PresItemDetalleDTO>>(detalles));
        }

        // POST: api/PresItemDetalle
        [HttpPost]
        public async Task<ActionResult> Create(POST_PresItemDetalleDTO dto)
        {
            var entidad = _mapper.Map<PresItemDetalle>(dto);
            await _repositorio.AddAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id = entidad.Id }, _mapper.Map<GET_PresItemDetalleDTO>(entidad));
        }

        // PUT: api/PresItemDetalle/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PUT_PresItemDetalleDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID del detalle no coincide.");

            var entidad = await _repositorio.GetByIdAsync(id);
            if (entidad == null) return NotFound();

            _mapper.Map(dto, entidad);
            await _repositorio.UpdateAsync(entidad);

            return NoContent();
        }

        // DELETE: api/PresItemDetalle/{id}
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
