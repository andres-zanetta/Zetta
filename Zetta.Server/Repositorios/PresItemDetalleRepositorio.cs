using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public class PresItemDetalleRepositorio : Repositorio<PresItemDetalle>, IPresItemDetalleRepositorio
    {
        private readonly Context _context;

        public PresItemDetalleRepositorio(Context context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los detalles asociados a un presupuesto específico.
        /// </summary>
        public async Task<IEnumerable<PresItemDetalle>> GetByPresupuestoIdAsync(int presupuestoId)
        {
            return await _context.PresItemDetalles
                .Where(x => x.PresupuestoId == presupuestoId)
                .Include(x => x.ItemPresupuesto)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los detalles que usan un ítem de catálogo específico.
        /// </summary>
        public async Task<IEnumerable<PresItemDetalle>> GetByItemPresupuestoIdAsync(int itemPresupuestoId)
        {
            return await _context.PresItemDetalles
                .Where(x => x.ItemPresupuestoId == itemPresupuestoId)
                .Include(x => x.Presupuesto)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un detalle por su identificador, incluyendo sus relaciones.
        /// </summary>
        public async Task<PresItemDetalle?> GetDetalleByIdAsync(int id)
        {
            return await _context.PresItemDetalles
                .Include(x => x.ItemPresupuesto)
                .Include(x => x.Presupuesto)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Inserta un nuevo detalle en la base de datos.
        /// </summary>
        public override async Task<int> AddAsync(PresItemDetalle entity)
        {
            await _context.PresItemDetalles.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Inserta un nuevo detalle utilizando la convención Insert.
        /// </summary>
        public override async Task<int> Insert(PresItemDetalle entity)
        {
            await _context.PresItemDetalles.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un detalle existente.
        /// </summary>
        public override async Task UpdateAsync(PresItemDetalle entity)
        {
            _context.PresItemDetalles.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un detalle por su ID.
        /// </summary>
        public override async Task DeleteAsync(int id)
        {
            var entity = await _context.PresItemDetalles.FindAsync(id);
            if (entity != null)
            {
                _context.PresItemDetalles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retorna todos los registros de la tabla PresItemsDetalle.
        /// </summary>
        public override async Task<List<PresItemDetalle>> SelectAllAsync()
        {
            return await _context.PresItemDetalles
                .Include(x => x.ItemPresupuesto)
                .Include(x => x.Presupuesto)
                .ToListAsync();
        }
    }
}
