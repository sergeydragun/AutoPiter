using AutoPiter.Application.DTO;

namespace AutoPiter.Application.Interfaces
{
    public interface IEmployeeService : IBaseService
    {
        public Task<List<EmployeeDTO>> GetAllEmployeesAsync();
    }
}
