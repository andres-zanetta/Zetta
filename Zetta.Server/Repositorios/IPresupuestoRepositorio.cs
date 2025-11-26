using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IPresupuestoRepositorio : IRepositorio<Presupuesto>
    {
        Task DeleteAsync(int id);
        Task EliminarDefinitivamenteAsync(int id);
        Task<IEnumerable<Presupuesto>> GetInactivosAsync();
        Task<Presupuesto?> GetPresupuestoConDetallesPorIdAsync(int id);
        Task<IEnumerable<Presupuesto>> GetPresupuestosConDetallesAsync();
        Task<IEnumerable<Presupuesto>> GetPresupuestosPorClienteIdAsync(int clienteId);
        Task<IEnumerable<Presupuesto>> GetPresupuestosPorEstadoAsync(string estado);
        Task<bool> PresupuestoExisteAsync(int id);
        Task RestaurarAsync(int id);
        Task<List<Presupuesto>> SelectAllAsync();
        Task<Presupuesto?> SelectById(int id);
    }
}