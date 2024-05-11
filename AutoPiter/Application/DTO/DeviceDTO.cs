using AutoPiter.Domain.Enums;

namespace AutoPiter.Application.DTO
{
    public class DeviceDTO
    {
        /// <summary>
        /// Id устройства
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование устройства
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип соединения. 0 - Локальное соединение, 1 - сетевое соединение
        /// </summary>
        public ConnectionType ConnectionType { get; set; }
        /// <summary>
        /// MAC адрес сетевого устройства (при наличии)
        /// </summary>
        public string? MACAddress { get; set; }
    }
}
