using AutoPiter.Domain.Entities;

namespace AutoPiter.Domain.Interfaces
{
    public interface IRepositoryBranchAndDevice : IRepository<BranchAndDevice>
    {
        Task<int> GetMaxSerialNumberByBranch(Guid BranchId);
        Task SetDefaultFalseAllBranchDevices(Guid BranchId);
        Task PickDefaultInBranchAsync(Guid BranchId);
        Task<Guid> DeleteByIdAsync(Guid Id);
    }
}
