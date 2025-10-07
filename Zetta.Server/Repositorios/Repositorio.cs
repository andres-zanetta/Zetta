using Microsoft.EntityFrameworkCore;
using Zetta.BD.DATA;

namespace Zetta.Server.Repositorios
{
    /// <summary>
    /// Clase genérica que implementa el patrón Repositorio para acceder a la base de datos.
    /// Permite realizar operaciones CRUD (Create, Read, Update, Delete) de manera genérica
    /// para cualquier entidad que se pase como tipo genérico T.
    /// </summary>
    public class Repositorio<T> : IRepositorio<T> where T : class, IEntityBase
    {
        /// <summary>
        /// Contexto de base de datos de Entity Framework Core.
        /// Protected para permitir el acceso desde clases heredadas.
        /// </summary>
        protected readonly Context _context;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos.
        /// </summary>
        public Repositorio(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los registros de la entidad T desde la base de datos de forma asíncrona.
        /// </summary>
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Busca un registro por su ID. Devuelve null si no se encuentra.
        /// </summary>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Agrega una nueva entidad a la base de datos y devuelve el ID generado.
        /// </summary>
        public virtual async Task<int> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            var property = entity.GetType().GetProperty("Id");
            return property != null ? (int)property.GetValue(entity)! : 0;
        }

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una entidad de la base de datos a partir de su ID.
        /// </summary>
        public virtual async Task DeleteAsync(int id)
        {
            var z = await GetByIdAsync(id);
            if (z != null)
            {
                _context.Set<T>().Remove(z);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Devuelve todos los registros de la tabla asociada a la entidad T.
        /// </summary>
        public virtual async Task<List<T>> SelectAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Inserta una nueva entidad en la base de datos y devuelve el identificador generado.
        /// </summary>
        public virtual async Task<int> Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            var property = entity.GetType().GetProperty("Id");
            return property != null ? (int)property.GetValue(entity)! : 0;
        }
    }
}
