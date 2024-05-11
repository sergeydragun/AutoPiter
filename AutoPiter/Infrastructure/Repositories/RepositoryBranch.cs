using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;

namespace AutoPiter.Infrastructure.Repositories
{
    public class RepositoryBranch : Repository<Branch>, IRepositoryBranch
    {
        public RepositoryBranch(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
