namespace AutoPiter.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBranch RepositoryBranch { get; }
        IRepositoryBranchAndDevice RepositoryBranchAndDevice { get; }
        IRepositoryDevice RepositoryDevice { get; }
        IRepositoryEmployee RepositoryEmployee { get; }
        IRepositoryPrintJob RepositoryPrintJob { get; }
        Task<int> SaveAsync();
    }
}
