using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoPiter.Infrastructure.Repositories
{
    public abstract class Repository<T>(DatabaseContext databaseContext) : IRepository<T> where T : class
    {
        protected DatabaseContext _databaseContext = databaseContext;

        public void Create(T entity)
        {
            _databaseContext.Set<T>().Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _databaseContext.AddAsync(entity);
        }

        public IQueryable<T> FindWithException(Expression<Func<T, bool>> exception)
        {
            return _databaseContext.Set<T>().Where(exception);
        }

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> exception)
        {
            return _databaseContext.Set<T>().FirstOrDefaultAsync(exception);
        }

        public IQueryable<T> GetAll()
        {
            return _databaseContext.Set<T>();
        }

        public void Update(T entity)
        {
            _databaseContext.Set<T>().Update(entity);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_databaseContext != null)
                {
                    _databaseContext.Dispose();
                    _databaseContext = null;
                }
            }
        }
    }
}
