using Zetta.Shared.DTOS.Presupuesto;


namespace Zetta.Client.Servicios
{
    public interface IPresupuestoServices
    {
        Task<int> Create(POST_PresupuestoDTO presupuesto);
        Task<List<GET_PresupuestoDTO>?> GetAll();
        Task<GET_PresupuestoDTO?> GetById(int id);
        Task Update(int id, PUT_PresupuestoDTO presupuesto);
        Task Delete(int id);
    }
}