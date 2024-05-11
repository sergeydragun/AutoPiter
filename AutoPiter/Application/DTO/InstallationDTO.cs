namespace AutoPiter.Application.DTO
{
    public class InstallationDTO
    {
        /// <summary>
        /// Id инсталляции
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Id филиала к которому привязана инсталляция
        /// </summary>
        public Guid BranchId { get; set; }
        /// <summary>
        /// Id устройства, к которому привязана инсталляция
        /// </summary>
        public Guid DeviceId { get; set; }
        /// <summary>
        /// Наименование инсталляции (Имя устройства внутри филиала)
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// Серийный номер устройства внутри филиала
        /// </summary>
        public int SerialNumber { get; set; }
        /// <summary>
        /// Является ли устройством по умолчанию
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
