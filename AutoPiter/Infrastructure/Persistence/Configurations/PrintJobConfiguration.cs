using AutoPiter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPiter.Infrastructure.Persistence.Configurations
{
    public class PrintJobConfiguration : IEntityTypeConfiguration<PrintJob>
    {
        public void Configure(EntityTypeBuilder<PrintJob> builder)
        {
            builder.HasOne(e => e.Employee)
                   .WithMany(e => e.PrintJobs)
                   .HasForeignKey(e => e.EmployeeId);

            builder.HasOne(e => e.Device)
                   .WithMany(e => e.PrintJobs)
                   .HasForeignKey(e => e.DeviceId);
        }
    }
}
