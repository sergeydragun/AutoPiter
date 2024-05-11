namespace AutoPiter.Application.DTO
{
    public class PrintJobCreateDTO
    {
        /// <summary>
        /// Наименование печати
        /// </summary>
        public string PrintJobName { get; set; }
        /// <summary>
        /// Id сотрудника филиала, в котором происходит печать
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Порядковый (серийный) номер устройства в филиалае
        /// </summary>
        public int? DeviceSerialNumber { get; set; }
        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PagesCount { get; set; }
    }
}
