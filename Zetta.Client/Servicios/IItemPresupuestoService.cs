using Zetta.Shared.DTOS.ItemPresupuesto;

namespace Zetta.Client.Servicios
{
    public interface IItemPresupuestoService
    {
        Task Create(POST_ItemPresupuestoDTO item);
        Task Delete(int id);
        Task<List<GET_ItemPresupuestoDTO>?> GetAll();
        Task<GET_ItemPresupuestoDTO?> GetById(int id);
        Task Update(int id, PUT_ItemPresupuestoDTO item);
        Task AplicarAumento(POST_AumentoMasivoDTO dto);
        Task<List<GET_ItemPresupuestoDTO>?> GetInactivos();
        Task Restaurar(int id);
    }
}