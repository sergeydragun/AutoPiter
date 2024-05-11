using AutoPiter.Domain.Entities;

namespace AutoPiter.Domain.Interfaces
{
    public interface IRepositoryEmployee : IRepository<Employee>
    {
        Task<Guid> GetDeviceIdByEmployeeAndNumber(Guid employeeId, int? serialNumber);
    }
}
