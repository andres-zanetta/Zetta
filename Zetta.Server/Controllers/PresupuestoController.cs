using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA; // Namespace del Context
using Zetta.BD.DATA.ENTITY; // Namespace de Entidades
using Zetta.Server.Repositorios; // Namespace de Repositorios
using Zetta.Shared.DTOS.Presupuesto; // Namespace de DTOs

namespace SERVER.Controllers // O el namespace correcto de tu controlador
{
    [ApiController]
    [Route("api/presupuestos")]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoRepositorio _presupuestoRepo;
        private readonly IMapper _mapper;
        private readonly Context _context; // Necesitamos el DbContext para SaveChangesAsync

        // Inyectar DbContext junto con el repositorio y mapper
        public PresupuestoController(Context context, IPresupuestoRepositorio presupuestoRepositorio, IMapper mapper)
        {
            _presupuestoRepo = presupuestoRepositorio;
            _mapper = mapper;
            _context = context; // Guardar la instancia del DbContext
        }

        // GET: api/presupuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GET_PresupuestoDTO>>> GetAll()
        {
            var presupuestos = await _presupuestoRepo.GetPresupuestosConDetallesAsync();
            var presupuestosDTO = _mapper.Map<IEnumerable<GET_PresupuestoDTO>>(presupuestos);
            return Ok(presupuestosDTO);
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

                // El repositorio agrega la entidad al DbContext (en memoria)
                await _presupuestoRepo.AddAsync(entidad);

                // El DbContext guarda los cambios en la BD
                await _context.SaveChangesAsync(); // <-- CORRECCIÓN: Usar _context

                return Ok(entidad.Id); // Devolver el ID generado
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en POST api/presupuestos: {ex}");
                return BadRequest($"Error al crear el presupuesto: {ex.Message}");
            }
        }

        // PUT: api/presupuestos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PUT_PresupuestoDTO dto) // Usar DTO para PUT
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo.");
            }

            // Traer la entidad existente CON sus detalles
            var presupuestoExistente = await _presupuestoRepo.GetPresupuestoConDetallesPorIdAsync(id);
            if (presupuestoExistente == null)
            {
                return NotFound($"Presupuesto con ID {id} no encontrado.");
            }

            try
            {
                // Mapear DTO a la entidad existente
                _mapper.Map(dto, presupuestoExistente);

                // El repositorio marca la entidad como modificada (si UpdateAsync lo hace)
                // Opcionalmente, podrías simplemente confiar en el seguimiento de cambios de EF Core
                // await _presupuestoRepo.UpdateAsync(presupuestoExistente); // Podría no ser necesario si Map actualiza el objeto trackeado

                // El DbContext guarda los cambios en la BD
                await _context.SaveChangesAsync(); // <-- CORRECCIÓN: Usar _context

                return NoContent(); // Respuesta estándar para PUT exitoso
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en PUT api/presupuestos/{id}: {ex}");
                return BadRequest($"Error al actualizar el presupuesto: {ex.Message}");
            }
        }

        // DELETE: api/presupuestos/{id}
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
                // El repositorio marca la entidad para eliminación
                await _presupuestoRepo.DeleteAsync(id);

                // El DbContext guarda los cambios en la BD
                await _context.SaveChangesAsync(); // <-- CORRECCIÓN: Usar _context

                return NoContent(); // Respuesta estándar para DELETE exitoso
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en DELETE api/presupuestos/{id}: {ex}");
                return BadRequest($"Error al eliminar el presupuesto: {ex.Message}");
            }
        }
    }
}