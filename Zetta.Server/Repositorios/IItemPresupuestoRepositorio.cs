using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IItemPresupuestoRepositorio : IRepositorio<ItemPresupuesto>
    {
        Task AplicarAumentoMasivo(string? marca, decimal porcentaje);
        Task DeleteAsync(int id);
        Task<bool> ExisteItemSimilar(string nombre, string marca, string medida);
        Task<List<ItemPresupuesto>> GetAllAsync();
        Task<List<ItemPresupuesto>> GetItemsPorNombreAsync(string nombre);
        Task<bool> ItemPresupuestoExisteAsync(int id);
        Task<ItemPresupuesto?> SelectById(int id);
        Task<List<ItemPresupuesto>> GetInactivosAsync();
        Task RestaurarItemAsync(int id);
    }
}