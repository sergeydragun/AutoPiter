using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AutoPiter.Application.Services
{
    public abstract class BaseService : IBaseService
    {
        protected IUnitOfWork _unitOfWork;
        protected ILogger<IBaseService> _logger;
        protected IMemoryCache _cache;

        public BaseService(IUnitOfWork unitOfWork,
                           ILogger<IBaseService> logger,
                           IMemoryCache cache) : this(unitOfWork, logger)
        {
            _cache = cache;
        }

        public BaseService(IUnitOfWork unitOfWork,
                           ILogger<IBaseService> logger) : this(unitOfWork)
        {
            _logger = logger;
        }

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected T CacheEntity<T>(string key, T entity)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1) + TimeSpan.FromMinutes(45));

            _cache.Set(key, entity, cacheEntryOptions);
            return entity;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }
            }
        }
    }
}
