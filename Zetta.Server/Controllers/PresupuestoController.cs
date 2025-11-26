using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.Server.Repositorios;
using Zetta.Shared.DTOS.Presupuesto;

namespace SERVER.Controllers
{
    [ApiController]
    [Route("api/presupuestos")]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoRepositorio _presupuestoRepo;
        private readonly IMapper _mapper;
        private readonly Context _context;

        public PresupuestoController(Context context, IPresupuestoRepositorio presupuestoRepositorio, IMapper mapper)
        {
            _presupuestoRepo = presupuestoRepositorio;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/presupuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GET_PresupuestoDTO>>> GetAll()
        {
            var presupuestos = await _presupuestoRepo.GetPresupuestosConDetallesAsync();
            var presupuestosDTO = _mapper.Map<IEnumerable<GET_PresupuestoDTO>>(presupuestos);
            return Ok(presupuestosDTO);
        }

        // GET: api/presupuestos/papelera
        [HttpGet("papelera")]
        public async Task<ActionResult<IEnumerable<GET_PresupuestoDTO>>> GetPapelera()
        {
            var presupuestos = await _presupuestoRepo.GetInactivosAsync();
            var dtos = _mapper.Map<IEnumerable<GET_PresupuestoDTO>>(presupuestos);
            return Ok(dtos);
        }

        // GET: api/presupuestos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GET_PresupuestoDTO>> GetById(int id)
        {
            var presupuesto = await _presupuestoRepo.GetPresupuestoConDetallesPorIdAsync(id);
            if (presupuesto == null)
            {
                return NotFound($"Presupuesto con ID {id} no encontrado.");
            }
            var presupuestoDTO = _mapper.Map<GET_PresupuestoDTO>(presupuesto);
            return Ok(presupuestoDTO);
        }

        // POST: api/presupuestos
        [HttpPost]
        public async Task<ActionResult<int>> Post(POST_PresupuestoDTO dto)
        {
            try
            {
                Presupuesto entidad = _mapper.Map<Presupuesto>(dto);
                entidad.FechaCreacion = DateTime.Now;
                entidad.Activo = true;
                await _presupuestoRepo.AddAsync(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en POST api/presupuestos: {ex}");
                return BadRequest($"Error al crear el presupuesto: {ex.Message}");
            }
        }

        // PUT: api/presupuestos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PUT_PresupuestoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            var presupuestoExistente = await _presupuestoRepo.GetPresupuestoConDetallesPorIdAsync(id);
            if (presupuestoExistente == null)
            {
                return NotFound($"Presupuesto con ID {id} no encontrado.");
            }

            try
            {
                _mapper.Map(dto, presupuestoExistente);
                await _presupuestoRepo.UpdateAsync(presupuestoExistente);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en PUT api/presupuestos/{id}: {ex}");
                return BadRequest($"Error al actualizar el presupuesto: {ex.Message}");
            }
        }

        // PUT: api/presupuestos/restaurar/5
        [HttpPut("restaurar/{id:int}")]
        public async Task<ActionResult> Restaurar(int id)
        {
            await _presupuestoRepo.RestaurarAsync(id);
            return NoContent();
        }

        // DELETE: api/presupuestos/{id} (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var presupuestoExiste = await _presupuestoRepo.PresupuestoExisteAsync(id);
            if (!presupuestoExiste)
            {
                return NotFound($"Presupuesto con ID {id} no encontrado.");
            }

            try
            {
                await _presupuestoRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en DELETE api/presupuestos/{id}: {ex}");
                return BadRequest($"Error al eliminar el presupuesto: {ex.Message}");
            }
        }

        // DELETE: api/presupuestos/definitivo/5
        [HttpDelete("definitivo/{id:int}")]
        public async Task<ActionResult> DeleteDefinitivo(int id)
        {
            try
            {
                await _presupuestoRepo.EliminarDefinitivamenteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar definitivamente: {ex.Message}");
            }
        }
    }
}