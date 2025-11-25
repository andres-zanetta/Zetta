using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;

namespace Zetta.Server.Repositorios
{
    public class ItemPresupuestoRepositorio : Repositorio<ItemPresupuesto>, IItemPresupuestoRepositorio
    {
        private readonly Context _context;

        public ItemPresupuestoRepositorio(Context context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<ItemPresupuesto>> GetAllAsync()
        {
            return await _context.ItemPresupuestos
                .Where(i => i.Activo) 
                .OrderBy(i => i.Nombre)
                .ToListAsync();
        }

        public override async Task DeleteAsync(int id)
        {
            var item = await _context.ItemPresupuestos.FindAsync(id);
            if (item != null)
            {
                item.Activo = false;
                _context.ItemPresupuestos.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ItemPresupuesto>> GetItemsPorNombreAsync(string nombre)
        {
            return await _context.ItemPresupuestos
                .Where(ip => ip.Activo && EF.Functions.Like(ip.Nombre.ToLower(), $"%{nombre.ToLower()}%"))
                .ToListAsync();
        }

        public async Task<bool> ItemPresupuestoExisteAsync(int id)
        {
            return await _context.ItemPresupuestos.AnyAsync(ip => ip.Id == id && ip.Activo);
        }

        public async Task<ItemPresupuesto?> SelectById(int id)
        {
            return await _context.ItemPresupuestos
                .FirstOrDefaultAsync(ip => ip.Id == id && ip.Activo);
        }

        public async Task<bool> ExisteItemSimilar(string nombre, string marca, string medida)
        {
            var nombreNorm = nombre.Trim().ToLower();
            var marcaNorm = (marca ?? "").Trim().ToLower();
            var medidaNorm = (medida ?? "").Trim().ToLower();

            return await _context.ItemPresupuestos
                .AnyAsync(i => i.Activo
                            && i.Nombre.ToLower().Trim() == nombreNorm
                            && (i.Marca == null ? "" : i.Marca.ToLower().Trim()) == marcaNorm
                            && (i.Medida == null ? "" : i.Medida.ToLower().Trim()) == medidaNorm);
        }

        public async Task AplicarAumentoMasivo(string? marca, decimal porcentaje)
        {
            var query = _context.ItemPresupuestos.Where(x => x.Activo);

            if (!string.IsNullOrWhiteSpace(marca))
            {
                query = query.Where(x => x.Marca.ToLower() == marca.ToLower());
            }

            var items = await query.ToListAsync();

            // Factor ej: 10% -> 1.10
            decimal factor = 1 + (porcentaje / 100m);
            var fechaHoy = DateTime.Now;

            foreach (var item in items)
            {
                if (item.Precio.HasValue)
                {
                    item.Precio = item.Precio.Value * factor;
                    item.FechActuPrecio = fechaHoy;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemPresupuesto>> GetInactivosAsync()
        {
            return await _context.ItemPresupuestos
                .Where(i => !i.Activo)
                .OrderBy(i => i.Nombre)
                .ToListAsync();
        }

        // Restaurar (Volver a Activo = true)
        public async Task RestaurarItemAsync(int id)
        {
            // Usamos IgnoreQueryFilters o buscamos directamente en el set
            // Como tu repositorio base usa _context.Set<T>(), podemos acceder directo
            var item = await _context.ItemPresupuestos.FindAsync(id);
            if (item != null)
            {
                item.Activo = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}