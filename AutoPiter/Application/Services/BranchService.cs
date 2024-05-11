using AutoPiter.Application.Converters;
using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AutoPiter.Application.Services
{
    public class BranchService : BaseService, IBranchService
    {
        public BranchService(IUnitOfWork unitOfWork, 
                             ILogger<IBaseService> logger) : base(unitOfWork, logger)
        {

        }

        public async Task<List<BranchDTO>> GetAllBranchesAsync()
        {
            var branches = await _unitOfWork.RepositoryBranch.GetAll().ToListAsync();
            return branches.Select(b => b.ConvertToDTO()).ToList();
        }
    }
}
