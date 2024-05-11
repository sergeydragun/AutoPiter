using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;

namespace AutoPiter.Infrastructure.Repositories
{
    public class RepositoryPrintJob : Repository<PrintJob>, IRepositoryPrintJob
    {
        public RepositoryPrintJob(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
