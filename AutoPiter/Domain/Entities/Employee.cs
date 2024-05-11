namespace AutoPiter.Domain.Entities
{
    public class Employee : AggregateRoot
    {
        public string Name { get; set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public List<PrintJob> PrintJobs { get; set; }
    }
}
