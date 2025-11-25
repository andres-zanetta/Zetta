using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public interface IObraRepositorio : IRepositorio<Obra>
    {
        Task<Obra?> ObtenerObraPorIdConDetallesAsync(int id);
        Task<IEnumerable<Obra>> ObtenerObrasConDetallesAsync();
        Task<IEnumerable<Obra>> ObtenerObrasPorClienteAsync(int clienteId);
        Task<IEnumerable<Obra>> ObtenerObrasPorEstadoAsync(EstadoObra estado);
    }
}