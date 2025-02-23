using Microsoft.EntityFrameworkCore;
using Data.Models;


namespace Data;

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

        modelBuilder.Entity<Project>()
            .HasIndex(p => p.ProjectNumber)
            .IsUnique();

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
            new Status { StatusId = 1, StatusName = "Not Started" },
            new Status { StatusId = 2, StatusName = "In Progress" },
            new Status { StatusId = 3, StatusName = "On Hold" },
            new Status { StatusId = 4, StatusName = "Completed" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Project Manager" },
            new Role { RoleId = 2, RoleName = "Senior Developer" },
            new Role { RoleId = 3, RoleName = "Business Analyst" },
            new Role { RoleId = 4, RoleName = "QA Engineer" },
            new Role { RoleId = 5, RoleName = "Consultant" }
        );

        modelBuilder.Entity<Unit>().HasData(
            new Unit { UnitId = 1, UnitName = "Hours" },
            new Unit { UnitId = 2, UnitName = "Days" },
            new Unit { UnitId = 3, UnitName = "Weeks" },
            new Unit { UnitId = 4, UnitName = "Months" }
         );

        modelBuilder.Entity<ServiceType>().HasData(
            new ServiceType { ServiceTypeId = 1, ServiceTypeName = "IT Consulting" },
            new ServiceType { ServiceTypeId = 2, ServiceTypeName = "Software Development" },
            new ServiceType { ServiceTypeId = 3, ServiceTypeName = "Project Management" },
            new ServiceType { ServiceTypeId = 4, ServiceTypeName = "Business Analysis" }
         );

        modelBuilder.Entity<ProjectManager>().HasData(
            new ProjectManager
            {
                ProjectManagerId = 1,
                FirstName = "Andreas",
                LastName = "Laine",
                Email = "andreas.laine@domain.com",
                RoleId = 1
            }
        );

        modelBuilder.Entity<Customer>().HasData(
             new Customer 
             { CustomerId = 1, 
                 CustomerName = "Borås Kommun", 
                 ContactPerson = "Anders Andersson" }
        );

        modelBuilder.Entity<Service>().HasData(
            new Service { ServiceId = 1, ServiceName = "IT Consulting", PricePerUnit = 1500, ServiceTypeId = 1, UnitId = 1 },
            new Service { ServiceId = 2, ServiceName = "Software Development", PricePerUnit = 2000, ServiceTypeId = 2, UnitId = 1 },
            new Service { ServiceId = 3, ServiceName = "Project Management", PricePerUnit = 2500, ServiceTypeId = 3, UnitId = 1 },
            new Service { ServiceId = 4, ServiceName = "Business Analysis", PricePerUnit = 1800, ServiceTypeId = 4, UnitId = 1 }
        );

        modelBuilder.Entity<Project>().HasData(
        new Project
        {
            ProjectId = 1,
            ProjectNumber = "P-1001",
            Name = "New mobile app",
            Description = "Update the software with a new mobile app",
            StartDate = new DateTime(2025, 02, 17),
            StatusId = 1,
            CustomerId = 1,
            ServiceId = 1,
            ProjectManagerId = 1,
            TotalPrice = 50000
        }
);

    }
}