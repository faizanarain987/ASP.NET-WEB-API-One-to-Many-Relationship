using Microsoft.EntityFrameworkCore;
using Task.Models.Entities;

namespace Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTask>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.EmployeeTasks)
                .HasForeignKey(t => t.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
