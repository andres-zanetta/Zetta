using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public class ObraRepositorio : Repositorio<Obra>, IObraRepositorio
    {
        private readonly Context context;

        public ObraRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // Obtener todas las obras con cliente y presupuesto
        public async Task<IEnumerable<Obra>> ObtenerObrasConDetallesAsync()
        {
            return await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Presupuesto)
                .ToListAsync();
        }

        // Obtener una obra por ID con todos sus datos relacionados
        public async Task<Obra?> ObtenerObraPorIdConDetallesAsync(int id)
        {
            return await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.Presupuesto)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // Filtrar obras por estado
        public async Task<IEnumerable<Obra>> ObtenerObrasPorEstadoAsync(EstadoObra estado)
        {
            return await _context.Obras
                .Where(o => o.EstadoObra == estado)
                .Include(o => o.Cliente)
                .Include(o => o.Presupuesto)
                .ToListAsync();
        }

        // Obtener todas las obras de un cliente específico
        public async Task<IEnumerable<Obra>> ObtenerObrasPorClienteAsync(int clienteId)
        {
            return await _context.Obras
                .Where(o => o.ClienteId == clienteId)
                .Include(o => o.Cliente)
                .Include(o => o.Presupuesto)
                .ToListAsync();
        }
    }
}