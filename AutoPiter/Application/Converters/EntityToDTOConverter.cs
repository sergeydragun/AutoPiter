using AutoPiter.Application.DTO;
using AutoPiter.Domain.Entities;

namespace AutoPiter.Application.Converters
{
    public static class EntityToDTOConverter
    {
        public static EmployeeDTO ConvertToDTO(this Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                BranchId = employee.BranchId,
            };
        }

        public static BranchDTO ConvertToDTO(this Branch branch)
        {
            return new BranchDTO
            {
                Id = branch.Id,
                Name = branch.Name,
                Location = branch.Location,
            };
        }

        public static DeviceDTO ConvertToDTO(this Device device)
        {
            return new DeviceDTO
            {
                Id = device.Id,
                Name = device.Name,
                ConnectionType = device.ConnectionType,
                MACAddress = device.MACAddress
            };
        }

        public static InstallationDTO ConvertToDTO(this BranchAndDevice installation)
        {
            return new InstallationDTO
            {
                Id = installation.Id,
                BranchId = installation.BranchId,
                DeviceId = installation.DeviceId,
                SerialNumber = installation.SerialNumber,
                DeviceName = installation.DeviceName,
                IsDefault = installation.IsDefault
            };
        }
    }
}
