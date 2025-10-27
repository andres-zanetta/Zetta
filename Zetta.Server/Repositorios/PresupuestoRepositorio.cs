using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public class PresupuestoRepositorio : Repositorio<Presupuesto>, IPresupuestoRepositorio
    {
        private readonly Context _context;

        public PresupuestoRepositorio(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Presupuesto?> GetPresupuestoConDetallesPorIdAsync(int id)
        {
            return await _context.Presupuestos
                .Include(p => p.Cliente) // <-- Agregado
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                // .Include(p => p.OpcionDePago) // <-- Eliminado: Es un enum, no se puede incluir
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Presupuesto>> GetPresupuestosConDetallesAsync()
        {
            return await _context.Presupuestos
                .Include(p => p.Cliente) // <-- Agregado
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                // .Include(p => p.OpcionDePago) // <-- Eliminado
                .ToListAsync();
        }

        // Reemplaza la línea problemática en el método GetPresupuestosPorClienteIdAsync
        public async Task<IEnumerable<Presupuesto>> GetPresupuestosPorClienteIdAsync(int clienteId)
        {
            // Solución: Verifica si la clase Presupuesto tiene una propiedad ClienteId (int) o similar.
            // Si existe, usa esa propiedad para filtrar. Si no existe, por favor proporciona la definición de la clase Presupuesto.
            return await _context.Presupuestos
                .Where(p => p.ClienteId == clienteId) // <-- Corregido: Era p.Id
                .Include(p => p.Cliente) // <-- Agregado
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                // .Include(p => p.OpcionDePago) // <-- Eliminado
                .ToListAsync();
        }

        public async Task<IEnumerable<Presupuesto>> GetPresupuestosPorEstadoAsync(string estado)
        {
            // Suponiendo que el estado se almacena en la propiedad Aceptado (true/false) o en otra propiedad
            // Si hay una propiedad específica de estado, reemplazar la condición por la correcta
            if (estado.Equals("Aceptado", StringComparison.OrdinalIgnoreCase))
            {
                return await _context.Presupuestos
                    .Where(p => p.Aceptado)
                    .Include(p => p.Cliente) // <-- Agregado
                    .Include(p => p.ItemsDetalle)
                        .ThenInclude(d => d.ItemPresupuesto)
                    // .Include(p => p.OpcionDePago) // <-- Eliminado
                    .ToListAsync();
            }
            else if (estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
            // Este bloque filtra los presupuestos cuyo estado es "Pendiente".
            // Es decir, selecciona aquellos presupuestos que NO han sido aceptados (p.Aceptado == false).
            // Además, incluye los detalles de los ítems y la opción de pago asociada a cada presupuesto.
            // Finalmente, devuelve la lista de presupuestos pendientes como resultado asíncrono.
            {
                return await _context.Presupuestos
                    .Where(p => !p.Aceptado)
                    .Include(p => p.Cliente) // <-- Agregado
                    .Include(p => p.ItemsDetalle)
                        .ThenInclude(d => d.ItemPresupuesto)
                    // .Include(p => p.OpcionDePago) // <-- Eliminado
                    .ToListAsync();
            }
            return new List<Presupuesto>();
        }

        public async Task<bool> PresupuestoExisteAsync(int id)
        {
            return await _context.Presupuestos.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Presupuesto>> SelectAllAsync()
        {
            return await _context.Presupuestos
                .Include(p => p.Cliente) // <-- Agregado
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                // .Include(p => p.OpcionDePago) // <-- Eliminado
                .ToListAsync();
        }

        public async Task<Presupuesto?> SelectById(int id)
        {
            Presupuesto? presupuesto = await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.Id == id);

            return presupuesto;
        }
    }
}