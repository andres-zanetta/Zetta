using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;


namespace Zetta.Server.Repositorios
{
    public class ItemPresupuestoRepositorio : Repositorio<ItemPresupuesto>, IItemPresupuestoRepositorio
    {
        private readonly Context _context;

        public ItemPresupuestoRepositorio(Context context) : base(context)
        {
            this._context = context;
        }

       public async Task<ItemPresupuesto?> GetByNameAsync(string nombre)
        {
            return await _context.ItemPresupuestos
                .FirstOrDefaultAsync(i => i.Nombre == nombre);
        }

        public async Task<IEnumerable<ItemPresupuesto>> GetAllAsync()
        {
            return await _context.ItemPresupuestos.ToListAsync();
        }
    }
}
