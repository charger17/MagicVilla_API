using MagicVilla_API.Data;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Crear(T entity)
        {
            await dbSet.AddAsync(entity);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(System.Linq.Expressions.Expression<Func<T, bool>> filtro = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> Obtenertodos(System.Linq.Expressions.Expression<Func<T, bool>>? filtro = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }

        public async Task Remover(T entity)
        {
            dbSet.Remove(entity);
            await Grabar();
        }
    }
}
