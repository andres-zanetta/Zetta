using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IPresItemDetalleRepositorio : IRepositorio<PresItemDetalle>
    {
        Task<int> AddAsync(PresItemDetalle entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<PresItemDetalle>> GetByItemPresupuestoIdAsync(int itemPresupuestoId);
        Task<IEnumerable<PresItemDetalle>> GetByPresupuestoIdAsync(int presupuestoId);
        Task<PresItemDetalle?> GetDetalleByIdAsync(int id);
        Task<int> Insert(PresItemDetalle entity);
        Task<List<PresItemDetalle>> SelectAllAsync();
        Task UpdateAsync(PresItemDetalle entity);
    }
}