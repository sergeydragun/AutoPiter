namespace AutoPiter.Domain.Entities
{
    public class Branch : AggregateRoot
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public List<BranchAndDevice> BranchAndDevices { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
