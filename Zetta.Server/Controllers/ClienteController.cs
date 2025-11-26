using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.BD.DATA.REPOSITORY;
using Zetta.Shared.DTOS.Cliente;

namespace Zetta.Server.Controllers
{
    [ApiController]
    [Route("/api/Cliente")]
    public class ClientesController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientesController(Context context, IMapper mapper, IClienteRepositorio clienteRepositorio)
        {
            _context = context;
            _mapper = mapper;
            _clienteRepositorio = clienteRepositorio;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<List<GET_ClienteDTO>>> Get()
        {
            var clientes = await _clienteRepositorio.GetAllAsync();
            var clientesDTO = _mapper.Map<List<GET_ClienteDTO>>(clientes);
            return Ok(clientesDTO);
        }

        // GET: api/Cliente/papelera
        [HttpGet("papelera")]
        public async Task<ActionResult<List<GET_ClienteDTO>>> GetPapelera()
        {
            var clientes = await _clienteRepositorio.GetInactivosAsync();
            var dtos = _mapper.Map<List<GET_ClienteDTO>>(clientes);
            return Ok(dtos);
        }

        // GET: api/clientes/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GET_ClienteDTO>> GetById(int id)
        {
            var cliente = await _clienteRepositorio.GetByIdAsync(id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            var clienteDTO = _mapper.Map<GET_ClienteDTO>(cliente);
            return Ok(clienteDTO);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] POST_ClienteDTO dto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(dto);
                cliente.Activo = true; 
                await _clienteRepositorio.AddAsync(cliente);
                return Ok(cliente.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/clientes/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PUT_ClienteDTO dto)
        {
            var dbCliente = await _clienteRepositorio.GetByIdAsync(id);
            if (dbCliente == null)
                return NotFound("Cliente no encontrado.");

            _mapper.Map(dto, dbCliente);

            try
            {
                await _clienteRepositorio.UpdateAsync(dbCliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Cliente/restaurar/5
        [HttpPut("restaurar/{id:int}")]
        public async Task<ActionResult> Restaurar(int id)
        {
            try
            {
                await _clienteRepositorio.RestaurarAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/clientes/5 (Soft Delete)
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _clienteRepositorio.GetByIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            try
            {
                await _clienteRepositorio.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Cliente/definitivo/5
        [HttpDelete("definitivo/{id:int}")]
        public async Task<ActionResult> DeleteDefinitivo(int id)
        {
            try
            {
                await _clienteRepositorio.EliminarDefinitivamenteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}