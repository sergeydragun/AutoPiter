namespace AutoPiter.Application.DTO
{
    public class EmployeeDTO
    {
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id филиала сотрудника
        /// </summary>
        public Guid BranchId { get; set; }
    }
}
