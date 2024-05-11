using AutoPiter.Domain.Enums;

namespace AutoPiter.Domain.Entities
{
    public class Device : AggregateRoot
    {
        public string Name { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public string? MACAddress { get; set; }
        public List<BranchAndDevice> BranchAndDevices { get; set; }
        public List<PrintJob> PrintJobs { get; set; }
    }
}
