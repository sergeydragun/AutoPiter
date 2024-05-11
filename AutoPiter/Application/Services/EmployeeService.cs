using AutoPiter.Application.Converters;
using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AutoPiter.Application.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork,
                               ILogger<IEmployeeService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees = await _unitOfWork.RepositoryEmployee.GetAll().ToListAsync();
            return employees.Select(e => e.ConvertToDTO()).ToList();
        }
    }
}
