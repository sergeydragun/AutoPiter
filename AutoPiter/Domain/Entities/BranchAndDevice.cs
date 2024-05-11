namespace AutoPiter.Domain.Entities
{
    public class BranchAndDevice : AggregateRoot
    {
        public Guid BranchId { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int SerialNumber { get; set; }
        public bool IsDefault { get; set; }

        public Branch Branch { get; set; }
        public Device Device { get; set; }
    }
}
