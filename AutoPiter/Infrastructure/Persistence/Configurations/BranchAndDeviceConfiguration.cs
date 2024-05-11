using AutoPiter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoPiter.Infrastructure.Persistence.Configurations
{
    public class BranchAndDeviceConfiguration : IEntityTypeConfiguration<BranchAndDevice>
    {
        public void Configure(EntityTypeBuilder<BranchAndDevice> builder)
        {
            builder.HasOne(e => e.Device)
                   .WithMany(e => e.BranchAndDevices)
                   .HasForeignKey(e => e.DeviceId);

            builder.HasOne(e => e.Branch)
                   .WithMany(e => e.BranchAndDevices)
                   .HasForeignKey(e => e.BranchId);
        }
    }
}
