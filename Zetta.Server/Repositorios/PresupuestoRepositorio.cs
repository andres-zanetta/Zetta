using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Presupuesto>> GetPresupuestosConDetallesAsync()
        {
            return await _context.Presupuestos
                .Where(p => p.Activo)
                .Include(p => p.Cliente)
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                .ToListAsync();
        }

        public async Task<Presupuesto?> GetPresupuestoConDetallesPorIdAsync(int id)
        {
            return await _context.Presupuestos
                .Where(p => p.Activo)
                .Include(p => p.Cliente)
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Soft Delete
        public override async Task DeleteAsync(int id)
        {
            var pres = await _context.Presupuestos.FindAsync(id);
            if (pres != null)
            {
                pres.Activo = false;
                _context.Presupuestos.Update(pres);
                await _context.SaveChangesAsync();
            }
        }

        // ---MÉTODOS DE PAPELERA ---

        public async Task<IEnumerable<Presupuesto>> GetInactivosAsync()
        {
            return await _context.Presupuestos
                .Where(p => !p.Activo)
                .Include(p => p.Cliente)
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                .ToListAsync();
        }

        public async Task RestaurarAsync(int id)
        {
            var pres = await _context.Presupuestos.FindAsync(id);
            if (pres != null)
            {
                pres.Activo = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarDefinitivamenteAsync(int id)
        {
            var pres = await _context.Presupuestos.FindAsync(id);
            if (pres != null)
            {
                _context.Presupuestos.Remove(pres);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> PresupuestoExisteAsync(int id)
        {
            return await _context.Presupuestos.AnyAsync(p => p.Id == id && p.Activo);
        }

        // Métodos heredados o de ayuda
        public async Task<IEnumerable<Presupuesto>> GetPresupuestosPorClienteIdAsync(int clienteId)
        {
            return await _context.Presupuestos
                .Where(p => p.ClienteId == clienteId && p.Activo)
                .Include(p => p.Cliente)
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Presupuesto>> GetPresupuestosPorEstadoAsync(string estado)
        {
            if (estado.Equals("Aceptado", StringComparison.OrdinalIgnoreCase))
            {
                return await _context.Presupuestos
                    .Where(p => p.Aceptado && p.Activo)
                    .Include(p => p.Cliente)
                    .Include(p => p.ItemsDetalle)
                        .ThenInclude(d => d.ItemPresupuesto)
                    .ToListAsync();
            }
            else if (estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
            {
                return await _context.Presupuestos
                    .Where(p => !p.Aceptado && p.Activo)
                    .Include(p => p.Cliente)
                    .Include(p => p.ItemsDetalle)
                        .ThenInclude(d => d.ItemPresupuesto)
                    .ToListAsync();
            }
            return new List<Presupuesto>();
        }

        public async Task<List<Presupuesto>> SelectAllAsync()
        {
            return await _context.Presupuestos
                .Where(p => p.Activo)
                .Include(p => p.Cliente)
                .Include(p => p.ItemsDetalle)
                    .ThenInclude(d => d.ItemPresupuesto)
                .ToListAsync();
        }

        public async Task<Presupuesto?> SelectById(int id)
        {
            return await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.Id == id && p.Activo);
        }
    }
}