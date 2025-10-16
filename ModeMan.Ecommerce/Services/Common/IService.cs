using System.Linq.Expressions;

namespace ModeMan.Ecommerce.Services.Common
{
    public interface IService<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAll();
        Task<T> CreateAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByExpressionAsync(
            Expression<Func<T, bool>> expression = null, 
            params Expression<Func<T, object>>[] expressions);
        T GetById(Guid id);
        T Update(T entity);
        void Delete(T entity);
    }
}
