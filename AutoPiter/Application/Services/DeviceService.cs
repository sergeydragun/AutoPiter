using AutoPiter.Application.Cache;
using AutoPiter.Application.Converters;
using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Entities;
using AutoPiter.Domain.Enums;
using AutoPiter.Domain.Exceptions;
using AutoPiter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace AutoPiter.Application.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IUnitOfWork unitOfWork, 
                             ILogger<IBaseService> logger, 
                             IMemoryCache cache) : base(unitOfWork, logger, cache)
        {

        }

        public async Task<Guid> AddDeviceInstallation(InstallationCreateDTO installationDataDTO)
        {
            await CheckBranchIdAndDeviceId(installationDataDTO.BranchId, installationDataDTO.DeviceId);

            var branchAndDevice = new BranchAndDevice
            {
                BranchId = installationDataDTO.BranchId,
                DeviceId = installationDataDTO.DeviceId,
                DeviceName = installationDataDTO.InstallationName,
                IsDefault = installationDataDTO.IsDefault
            };

            if (installationDataDTO.SerialNumber == null)
            {
                var maxNumber = await _unitOfWork.RepositoryBranchAndDevice.GetMaxSerialNumberByBranch(installationDataDTO.BranchId);
                branchAndDevice.SerialNumber = maxNumber + 1;
            }
            else 
            {
                await CheckSerialNumberNotExistInBranch(installationDataDTO.BranchId, installationDataDTO.SerialNumber ?? 1);
                branchAndDevice.SerialNumber = installationDataDTO.SerialNumber ?? 1;
            }

            if (installationDataDTO.IsDefault)
            {
                await _unitOfWork.RepositoryBranchAndDevice.SetDefaultFalseAllBranchDevices(installationDataDTO.BranchId);
            }

            await _unitOfWork.RepositoryBranchAndDevice.CreateAsync(branchAndDevice);
            await _unitOfWork.SaveAsync();

            await RefreshInstallationCache(installationDataDTO.BranchId);

            return branchAndDevice.Id;
        }

        public async Task<List<DeviceDTO>> GetAllDevicesAsync()
        {
            var devices = await _unitOfWork.RepositoryDevice.GetAll().ToListAsync();
            return devices.Select(d => d.ConvertToDTO()).ToList();
        }

        public async Task<List<DeviceDTO>> GetDevicesByConnectionTypeAsync(ConnectionType connectionType)
        {
            var devices = await _unitOfWork.RepositoryDevice.FindWithException(d => d.ConnectionType == connectionType)
                                                            .ToListAsync();

            return devices.Select(d => d.ConvertToDTO()).ToList();
        }

        public async Task CheckBranchIdAndDeviceId(Guid BranchId, Guid DeviceId)
        {
            var hasBranch = await _unitOfWork.RepositoryBranch.GetAll()
                                                              .AnyAsync(b => b.Id == BranchId);

            if (!hasBranch)
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Entity did not found", "Такого филиала не существует");
            }

            var hasDevice = await _unitOfWork.RepositoryDevice.GetAll()
                                                              .AnyAsync(b => b.Id == DeviceId);

            if (!hasDevice)
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Entity did not found", "Такого устройства не существует");
            }
        }

        public async Task CheckSerialNumberNotExistInBranch(Guid BranchId, int SerialNumber)
        {
            var isExist = await _unitOfWork.RepositoryBranchAndDevice.GetAll()
                                                                     .AnyAsync(b => b.BranchId ==  BranchId && 
                                                                                    b.SerialNumber == SerialNumber);

            if (isExist)
            {
                throw new ApiException("serial number is already exist", "Устройство с таким серийным номером уже существует");
            }
        }

        public async Task<List<InstallationDTO>> GetInstallationsAsync(Guid? BranchId)
        {
            if(BranchId == null)
            {
                var key = CacheKeys.InstallationsListBKey();

                var installations = new List<BranchAndDevice>();

                if (!_cache.TryGetValue(key, out List<BranchAndDevice> result))
                {
                    installations = await _unitOfWork.RepositoryBranchAndDevice.GetAll().ToListAsync();
                    _ = CacheEntity(key, installations);
                    return installations.Select(i => i.ConvertToDTO()).ToList();
                }

                installations = result;

                return installations.Select(i => i.ConvertToDTO()).ToList();
            }
            else 
            {
                var key = CacheKeys.InstallationsListByBranchKey(BranchId??default);

                var installations = new List<BranchAndDevice>();

                if (!_cache.TryGetValue(key, out List<BranchAndDevice> result))
                {
                    installations = await _unitOfWork.RepositoryBranchAndDevice.FindWithException(b => b.BranchId == BranchId)
                                                                               .ToListAsync();
                    _ = CacheEntity(key, installations);
                    return installations.Select(i => i.ConvertToDTO()).ToList();
                }

                installations = result;

                return installations.Select(i => i.ConvertToDTO()).ToList();
            }
        }

        public async Task<InstallationDTO> GetInstallationByIdAsync(Guid Id)
        {
            string key = CacheKeys.InstallationKey(Id);

            BranchAndDevice installation;

            if (!_cache.TryGetValue(key, out BranchAndDevice result))
            {
                installation = await _unitOfWork.RepositoryBranchAndDevice.FirstOrDefaultAsync(b => b.Id == Id);
                _ = CacheEntity(key, installation);
                return installation.ConvertToDTO();
            }

            installation = result;

            return installation.ConvertToDTO();
        }

        public async Task DeleteInstallationAsync(Guid Id)
        {
            var branchId = await _unitOfWork.RepositoryBranchAndDevice.DeleteByIdAsync(Id);

            await _unitOfWork.SaveAsync();

            await RefreshInstallationCache(branchId);
        }

        private async Task RefreshInstallationCache(Guid BranchId)
        {
            var installations = await _unitOfWork.RepositoryBranchAndDevice.FindWithException(b => b.BranchId == BranchId)
                                                                           .ToListAsync();

            var listKey = CacheKeys.InstallationsListByBranchKey(BranchId);

            _ = CacheEntity(listKey, installations);

            foreach(var installation in installations)
            {
                var key = CacheKeys.InstallationKey(installation.Id);

                _ = CacheEntity(key, installation);
            }
        }
    }
}
