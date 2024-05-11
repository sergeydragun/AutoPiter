using AutoPiter.Application.DTO;
using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Exceptions;
using AutoPiter.Domain.Interfaces;
using AutoPiter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AutoPiter.Infrastructure.Repositories
{
    public class RepositoryEmployee : Repository<Employee>, IRepositoryEmployee
    {
        public RepositoryEmployee(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Guid> GetDeviceIdByEmployeeAndNumber(Guid employeeId, int? serialNumber)
        {
            BranchAndDevice? branchAndDevice;

            if (serialNumber != null)
            {
                branchAndDevice = await _databaseContext.Employees.Where(e => e.Id == employeeId)
                                                                  .Include(e => e.Branch)
                                                                  .ThenInclude(b => b.BranchAndDevices.Where(bd => bd.SerialNumber == serialNumber))
                                                                  .SelectMany(e => e.Branch.BranchAndDevices)
                                                                  .FirstOrDefaultAsync();

                if (branchAndDevice == null)
                {
                    throw new ApiException("Default device not exist", "Не найдено устройство с таким серийным номером");
                }
            }
            else
            {
                branchAndDevice = await _databaseContext.Employees.Where(e => e.Id == employeeId)
                                                                  .Include(e => e.Branch)
                                                                  .ThenInclude(b => b.BranchAndDevices.Where(bd => bd.IsDefault))
                                                                  .SelectMany(e => e.Branch.BranchAndDevices)
                                                                  .FirstOrDefaultAsync();

                if (branchAndDevice == null)
                {
                    throw new ApiException("Default device not exist", "Отсутствует устройство по умолчанию");
                }
            }

            return branchAndDevice.DeviceId;
        }
    }
}
