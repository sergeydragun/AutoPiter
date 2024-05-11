namespace AutoPiter.Application.DTO
{
    public class BranchDTO
    {
        /// <summary>
        /// Id филиала
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование филиала
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Локация филиала (вероятнее всего совпадает с наименованием)
        /// </summary>
        public string Location { get; set; }
    }
}
