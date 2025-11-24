using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public class VisitaTecnicaRepositorio : Repositorio<VisitaTecnica>, IVisitaTecnicaRepositorio
    {
        public VisitaTecnicaRepositorio(Context context) : base(context)
        {
        }

        public async Task<List<VisitaTecnica>> GetAllConDetallesAsync()
        {
            return await _context.VisitasTecnicas
                .Include(v => v.Cliente) // Incluimos el cliente para ver el Nombre
                .OrderByDescending(v => v.FechaVisita)
                .ToListAsync();
        }

        // Sobreescribimos GetById para traer el cliente al editar
        public override async Task<VisitaTecnica?> GetByIdAsync(int id)
        {
            return await _context.VisitasTecnicas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
