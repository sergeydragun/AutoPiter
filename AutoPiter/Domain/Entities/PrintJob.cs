using AutoPiter.Domain.Statuses;

namespace AutoPiter.Domain.Entities
{
    public class PrintJob : AggregateRoot
    {
        public string PrintJobName { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid DeviceId { get; set; }
        public int PagesCount { get; set; }
        public JobStatus Status { get; set; }

        public Employee Employee { get; set; }
        public Device Device { get; set; }
    }
}
