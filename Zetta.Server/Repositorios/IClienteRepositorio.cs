using Zetta.BD.DATA.ENTITY;
using Zetta.Server.Repositorios;

namespace Zetta.BD.DATA.REPOSITORY
{
    public interface IClienteRepositorio  : IRepositorio<Cliente>
    {
        Task DeleteAsync(int id);
        Task EliminarDefinitivamenteAsync(int id);
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<List<Cliente>> GetInactivosAsync();
        Task RestaurarAsync(int id);
        Task<Cliente?> SearchByNameAsync(string nombre);
        Task<Cliente?> SelectByEmailAsync(string email);
    }
}