using AutoPiter.Application.DTO;
using AutoPiter.Domain.Enums;

namespace AutoPiter.Application.Interfaces
{
    public interface IDeviceService : IBaseService
    {
        Task<List<DeviceDTO>> GetAllDevicesAsync();
        Task<List<DeviceDTO>> GetDevicesByConnectionTypeAsync(ConnectionType connectionType);
        Task<Guid> AddDeviceInstallation(InstallationCreateDTO installationDataDTO);
        Task CheckBranchIdAndDeviceId(Guid BranchId, Guid DeviceId);
        Task CheckSerialNumberNotExistInBranch(Guid BranchId, int SerialNumber);
        Task<List<InstallationDTO>> GetInstallationsAsync(Guid? BranchId);
        Task<InstallationDTO> GetInstallationByIdAsync(Guid Id);
        Task DeleteInstallationAsync(Guid Id);
    }
}
