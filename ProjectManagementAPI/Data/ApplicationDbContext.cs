using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public required DbSet<Project> Projects { get; set; }
    public required DbSet<Customer> Customers { get; set; }
    public required DbSet<Service> Services { get; set; }
    public required DbSet<ServiceType> ServiceTypes { get; set; }
    public required DbSet<Status> Statuses { get; set; }
    public required DbSet<ProjectManager> ProjectManagers { get; set; }
    public required DbSet<Role> Roles { get; set; }
    public required DbSet<ProjectMember> ProjectMembers { get; set; }
    public required DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectMember>()
            .HasKey(pm => new { pm.ProjectId, pm.ProjectManagerId });

        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project)
            .WithMany()
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectMember>()
           .HasOne(pm => pm.ProjectManager)
           .WithMany()
           .HasForeignKey(pm => pm.ProjectManagerId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = 1, StatusName = "New" },
            new Status { StatusId = 2, StatusName = "In Progress" },
            new Status { StatusId = 3, StatusName = "Completed" }
        );
    }

}
