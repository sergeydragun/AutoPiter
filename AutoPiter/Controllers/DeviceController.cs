using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using AutoPiter.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AutoPiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DeviceController(IDeviceService deviceService) : Controller
    {
        private readonly IDeviceService _deviceService = deviceService;

        /// <summary>
        /// Получает список устройств. Если указан тип соединения - фильтрует по нему.
        /// </summary>
        /// <param name="connectionType">Тип соединения. 0 - Локальное соединение, 1 - сетевое соединение</param>
        /// <returns>Список устройств</returns>
        /// <response code="200">Успешно вернул список устройств</response>
        [HttpGet]
        public async Task<ActionResult<List<DeviceDTO>>> GetDevices(ConnectionType? connectionType)
        {
            if (connectionType is null)
            {
                var devices = await _deviceService.GetAllDevicesAsync();
                return Ok(devices);
            }

            var conDevices = await _deviceService.GetDevicesByConnectionTypeAsync(connectionType??default);
            return Ok(conDevices);
        }

        /// <summary>
        /// Добавляет инсталляцию устройства
        /// </summary>
        /// <param name="installationDataDTO">Данные для добавления инсталляции</param>
        /// <returns>Id созданной инсталляции</returns>
        /// <response code="201">Успешно добавил инсталляцию</response>
        /// <response code="400">Неверное значение Id филиала или устройства</response>
        [HttpPost("add_installation")]
        public async Task<ActionResult<Guid>> AddInstallation(InstallationCreateDTO installationDataDTO)
        {
            var installationId = await _deviceService.AddDeviceInstallation(installationDataDTO);

            return new ObjectResult(installationId) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Список всех инсталляций, с возможной фильтрацией по Id филиала
        /// </summary>
        /// <param name="branchId">Id филиала</param>
        /// <returns>Список инсталляций</returns>
        /// <response code="200">Успешно вернул список инсталляций</response>
        [HttpGet("installations")]
        public async Task<ActionResult<List<InstallationDTO>>> GetInstallations(Guid? branchId)
        {
            var installation = await _deviceService.GetInstallationsAsync(branchId);
            return Ok(installation);
        }

        /// <summary>
        /// Данные инсталляции по Id
        /// </summary>
        /// <param name="Id">Id инсталляции</param>
        /// <returns>Данные инсталляции</returns>
        /// <response code="200">Успешно вернул данные инсталляции</response>
        [HttpGet("installation_by_id")]
        public async Task<ActionResult<InstallationDTO>> GetInstallationById(Guid Id)
        {
            var installation = await _deviceService.GetInstallationByIdAsync(Id);
            return Ok(installation);
        }

        /// <summary>
        /// Удаляет инсталяцию устройства по её Id
        /// </summary>
        /// <param name="Id">Id инсталляции</param>
        /// <returns>HTTP статус</returns>
        /// <response code="200">Успешно удалил инсталляцию</response>
        [HttpDelete("installation")]
        public async Task<ActionResult> DeleteInstallation(Guid Id)
        {
            await _deviceService.DeleteInstallationAsync(Id);
            return Ok();
        }
    }
}
