using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IVisitaTecnicaRepositorio : IRepositorio<VisitaTecnica>
    {
        Task<List<VisitaTecnica>> GetAllConDetallesAsync();
        Task<VisitaTecnica?> GetByIdAsync(int id);
    }
}