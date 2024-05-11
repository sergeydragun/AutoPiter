using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;

namespace AutoPiter.Infrastructure.Repositories
{
    public class UnitOfWork(DatabaseContext databaseContext) : IUnitOfWork
    {
        private IRepositoryBranch _repositoryBranch;
        public IRepositoryBranch RepositoryBranch 
        {
            get 
            { 
                _repositoryBranch ??= new RepositoryBranch(databaseContext);
                return _repositoryBranch;
            }
        }

        private IRepositoryBranchAndDevice _repositoryBranchAndDevice;
        public IRepositoryBranchAndDevice RepositoryBranchAndDevice
        {
            get
            {
                _repositoryBranchAndDevice ??= new RepositoryBranchAndDevice(databaseContext);
                return _repositoryBranchAndDevice;
            }
        }

        private IRepositoryDevice _repositoryDevice;
        public IRepositoryDevice RepositoryDevice
        {
            get
            {
                _repositoryDevice ??= new RepositoryDevice(databaseContext);
                return _repositoryDevice;
            }
        }

        private IRepositoryEmployee _repositoryEmployee;
        public IRepositoryEmployee RepositoryEmployee
        {
            get
            {
                _repositoryEmployee ??= new RepositoryEmployee(databaseContext);
                return _repositoryEmployee;
            }
        }

        private IRepositoryPrintJob _repositoryPrintJob;
        public IRepositoryPrintJob RepositoryPrintJob
        {
            get
            {
                _repositoryPrintJob ??= new RepositoryPrintJob(databaseContext);
                return _repositoryPrintJob;
            }
        }

        private DatabaseContext _databaseContext = databaseContext;
        private bool disposed = false;

        public Task<int> SaveAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    databaseContext.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
