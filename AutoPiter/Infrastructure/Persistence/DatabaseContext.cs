using AutoPiter.Domain.Entities;
using AutoPiter.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AutoPiter.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchAndDevice> BranchesAndDevices { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PrintJob> PrintJobs { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BranchAndDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PrintJobConfiguration());

            base.OnModelCreating(modelBuilder); 
        }
    }
}
