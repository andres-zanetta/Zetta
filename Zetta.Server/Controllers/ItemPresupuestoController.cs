using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.Server.Repositorios;
using Zetta.Shared.DTOS.ItemPresupuesto;

namespace Zetta.Server.Controllers
{
    [ApiController]
    [Route("/api/itempresupuesto")]
    public class ItemPresupuestoController : ControllerBase
    {
        // _context no es estrictamente necesario aquí si usamos el repositorio para todo,
        // pero lo dejo por si tu arquitectura base lo requería.
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IItemPresupuestoRepositorio _itemPresupuestoRepositorio;

        public ItemPresupuestoController(IItemPresupuestoRepositorio repositorio, Context context, IMapper mapper)
        {
            _itemPresupuestoRepositorio = repositorio;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ItemPresupuesto
        [HttpGet]
        public async Task<ActionResult<List<GET_ItemPresupuestoDTO>>> Get()
        {
            // El repositorio ya filtra por Activo = true
            var items = await _itemPresupuestoRepositorio.GetAllAsync();
            var itemsDTO = _mapper.Map<List<GET_ItemPresupuestoDTO>>(items);
            return Ok(itemsDTO);
        }

        // GET: api/ItemPresupuesto/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_ItemPresupuestoDTO>> GetById(int id)
        {
            // El repositorio ya filtra por Activo = true
            var item = await _itemPresupuestoRepositorio.SelectById(id); // Usamos el método específico si existe, o GetByIdAsync

            if (item == null)
            {
                return NotFound("No se encontró el ítem con ese ID.");
            }
            var itemDTO = _mapper.Map<GET_ItemPresupuestoDTO>(item);
            return Ok(itemDTO);
        }

        // POST: api/ItemPresupuesto
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] POST_ItemPresupuestoDTO dto)
        {
            // 1. VALIDACIÓN DE DUPLICADOS (NUEVO)
            // Antes de crear, verificamos si ya existe algo igual
            if (await _itemPresupuestoRepositorio.ExisteItemSimilar(dto.Nombre, dto.Marca ?? "", dto.Medida ?? ""))
            {
                return Conflict($"Ya existe un ítem con el nombre '{dto.Nombre}', marca '{dto.Marca}' y medida '{dto.Medida}'.");
            }

            try
            {
                var item = _mapper.Map<ItemPresupuesto>(dto);

                // Aseguramos que nazca activo
                item.Activo = true;
                if (!item.FechActuPrecio.HasValue) item.FechActuPrecio = DateTime.Now;

                var id = await _itemPresupuestoRepositorio.AddAsync(item);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/ItemPresupuesto/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_ItemPresupuestoDTO dto)
        {
            var item = await _itemPresupuestoRepositorio.GetByIdAsync(id);
            if (item == null)
                return NotFound("No se encontró el ítem.");

            _mapper.Map(dto, item);

            // Al editar, actualizamos la fecha de precio si cambió el precio
            // (Opcional: podrías comparar item.Precio != dto.Precio)
            item.FechActuPrecio = DateTime.Now;

            try
            {
                await _itemPresupuestoRepositorio.UpdateAsync(item);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ItemPresupuesto/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _itemPresupuestoRepositorio.GetByIdAsync(id);

            if (item == null)
                return NotFound("No se encontró el ítem con ese ID.");

            try
            {
                // El repositorio ahora hace "Soft Delete" (Activo = false)
                await _itemPresupuestoRepositorio.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el ítem: {e.Message}");
            }
        }

        // POST: api/ItemPresupuesto/aumento-masivo (NUEVO)
        [HttpPost("aumento-masivo")]
        public async Task<ActionResult> AumentoMasivo([FromBody] POST_AumentoMasivoDTO dto)
        {
            try
            {
                await _itemPresupuestoRepositorio.AplicarAumentoMasivo(dto.Marca, dto.Porcentaje);
                return Ok(new { mensaje = "Precios actualizados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("papelera")]
        public async Task<ActionResult<List<GET_ItemPresupuestoDTO>>> GetPapelera()
        {
            var items = await _itemPresupuestoRepositorio.GetInactivosAsync();
            var itemsDTO = _mapper.Map<List<GET_ItemPresupuestoDTO>>(items);
            return Ok(itemsDTO);
        }

        [HttpPut("restaurar/{id:int}")]
        public async Task<ActionResult> Restaurar(int id)
        {
            try
            {
                await _itemPresupuestoRepositorio.RestaurarItemAsync(id);
                return Ok(new { mensaje = "Ítem restaurado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}