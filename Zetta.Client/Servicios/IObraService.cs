using System.Net.Http;
using System.Net.Http.Json;
using Zetta.Shared.DTOS.Obra;

namespace Zetta.Client.Servicios
{
    public interface IObraService
    {
        Task<List<GET_ObraDTO>> GetAllAsync();
        Task<GET_ObraDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(POST_ObraDTO model);
        Task UpdateAsync(int id, PUT_ObraDTO model);
        Task DeleteAsync(int id);
    }
}