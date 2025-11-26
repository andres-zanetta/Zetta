using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zetta.BD.DATA;
using Zetta.BD.DATA.ENTITY;
using Zetta.Server.Repositorios;

namespace Zetta.BD.DATA.REPOSITORY
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly Context context;

        public ClienteRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        //GetAll para traer SOLO los activos (por defecto)
        public override async Task<List<Cliente>> GetAllAsync()
        {
            return await context.Clientes
                .Where(c => c.Activo)
                .Include(c => c.Presupuestos)
                .ToListAsync();
        }

        //GetById para traer solo activos
        public override async Task<Cliente?> GetByIdAsync(int id)
        {
            return await context.Clientes
                .Where(c => c.Activo)
                .Include(c => c.Presupuestos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Soft Delete (Papelera)
        public override async Task DeleteAsync(int id)
        {
            var cliente = await context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Activo = false;
                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();
            }
        }

        // Trae solo los clientes en papelera
        public async Task<List<Cliente>> GetInactivosAsync()
        {
            return await context.Clientes
                .Where(c => !c.Activo)
                .Include(c => c.Presupuestos)
                .ToListAsync();
        }

        // Restaura un cliente (Activo = true)
        public async Task RestaurarAsync(int id)
        {
            var cliente = await context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Activo = true;
                await context.SaveChangesAsync();
            }
        }

        // Elimina físicamente de la base de datos
        public async Task EliminarDefinitivamenteAsync(int id)
        {
            var cliente = await context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();
            }
        }

        // Métodos existentes de búsqueda
        public async Task<Cliente?> SearchByNameAsync(string nombre)
        {
            return await context.Clientes.FirstOrDefaultAsync(z => z.Nombre == nombre && z.Activo);
        }

        public async Task<Cliente?> SelectByEmailAsync(string email)
        {
            return await context.Clientes.FirstOrDefaultAsync(c => c.Email == email && c.Activo);
        }
    }
}