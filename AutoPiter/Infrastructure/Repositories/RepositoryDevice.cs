using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;

namespace AutoPiter.Infrastructure.Repositories
{
    public class RepositoryDevice : Repository<Device>, IRepositoryDevice
    {
        public RepositoryDevice(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
