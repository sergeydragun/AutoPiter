namespace AutoPiter.Application.DTO
{
    public class InstallationCreateDTO
    {
        /// <summary>
        /// Наименование инсталляции
        /// </summary>
        public string InstallationName { get; set; }
        /// <summary>
        /// Id филиала
        /// </summary>
        public Guid BranchId { get; set; }
        /// <summary>
        /// Id устройства
        /// </summary>
        public Guid DeviceId { get; set; }
        /// <summary>
        /// Порядковый номер устройства
        /// </summary>
        public int? SerialNumber { get; set; }
        /// <summary>
        /// Является ли устройством по умолчанию
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
