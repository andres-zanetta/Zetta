using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IItemPresupuestoRepositorio : IRepositorio<ItemPresupuesto>
    {
        Task<List<ItemPresupuesto>> SelectAllAsync();
        Task<ItemPresupuesto?> SelectById(int id);
        Task<int> Insert(ItemPresupuesto entity);
        Task<bool> ItemPresupuestoExisteAsync(int id);
        Task<List<ItemPresupuesto>> GetItemsPorRubroIdAsync(int rubroId);
        Task<List<ItemPresupuesto>> GetItemsPorNombreAsync(string nombre);
    }
}