using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AutoPiter.Infrastructure.Repositories
{
    public class RepositoryBranchAndDevice : Repository<BranchAndDevice>, IRepositoryBranchAndDevice
    {
        public RepositoryBranchAndDevice(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public Task<int> GetMaxSerialNumberByBranch(Guid BranchId)
        {
            return _databaseContext.BranchesAndDevices.Where(b => b.BranchId == BranchId)
                                                      .MaxAsync(b => b.SerialNumber);
        }

        public Task SetDefaultFalseAllBranchDevices(Guid BranchId)
        {
           return _databaseContext.BranchesAndDevices.Where(b => b.BranchId == BranchId)
                                                     .ExecuteUpdateAsync(b => b.SetProperty(e => e.IsDefault, e => false));
        }

        public async Task PickDefaultInBranchAsync(Guid BranchId)
        {

            var branchAndDevices = await _databaseContext.BranchesAndDevices.Where(b => b.BranchId == BranchId)
                                                                            .OrderBy(b => b.SerialNumber)
                                                                            .ToListAsync();

            if (!branchAndDevices.Any(b => b.IsDefault) && branchAndDevices.Count != 0)
            {
                branchAndDevices[0].IsDefault = true;
            }
        }

        public async Task<Guid> DeleteByIdAsync(Guid Id)
        {
            var entity = await _databaseContext.BranchesAndDevices.FirstOrDefaultAsync(b => b.Id == Id);

            await _databaseContext.BranchesAndDevices.Where(b => b.Id == Id)
                                                     .ExecuteDeleteAsync();

            if (entity.IsDefault)
            {
                await PickDefaultInBranchAsync(entity.BranchId);
            }

            return entity.BranchId;
        }
    }
}
