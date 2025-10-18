
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using System.Linq.Expressions;

namespace ModeMan.Ecommerce.Services.Common
{
    public class Service<T> : IService<T> where T : class, new()
    {
        protected readonly ModeManDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Service(ModeManDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByExpressionAsync(
            Expression<Func<T, bool>>? expression = null, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes.Any())
            {
                foreach (var include in includes)
                           query.Include(include);
            }

            if(expression != null)
                query = query.Where(expression);

            return await query.FirstOrDefaultAsync();
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

    }
}
