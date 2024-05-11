using AutoPiter.Application.DTO;

namespace AutoPiter.Application.Interfaces
{
    public interface IBranchService : IBaseService
    {
        public Task<List<BranchDTO>> GetAllBranchesAsync();
    }
}
