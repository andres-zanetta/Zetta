using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IItemPresupuestoRepositorio    
    {
        Task<IEnumerable<ItemPresupuesto>> ObtenerItemsPorPresupuestoIdAsync(int presupuestoId);
        Task EliminarItemsPorPresupuestoIdAsync(int presupuestoId);

        Task<ItemPresupuesto?> GetByNameAsync(string nombre);

        Task<IEnumerable<ItemPresupuesto>> GetAllAsync();

        Task<ItemPresupuesto?> GetByIdAsync(int id);

        Task<int> AddAsync(ItemPresupuesto entity);

        Task UpdateAsync(ItemPresupuesto entity);

        Task DeleteAsync(int id);



    }
}