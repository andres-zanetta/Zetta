using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IItemPresupuestoRepositorio
    {
        Task<IEnumerable<ItemPresupuesto>> GetAllAsync();
        Task<ItemPresupuesto?> GetByNameAsync(string nombre);
    }
}