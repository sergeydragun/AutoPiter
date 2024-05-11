using System.Linq.Expressions;

namespace AutoPiter.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindWithException(Expression<Func<T, bool>> exception);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> exception);
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
    }
}
