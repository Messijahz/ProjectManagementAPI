using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Data.Factories;

public static class ProjectFactory
{
    public static async Task<Project> CreateProjectAsync(ProjectInputDTO projectInputDTO, ApplicationDbContext context)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.CustomerName == projectInputDTO.CustomerName);
        var service = await context.Services.FirstOrDefaultAsync(s => s.ServiceName == projectInputDTO.ServiceName);
        var projectManager = await context.ProjectManagers.FirstOrDefaultAsync(pm => pm.FirstName + " " + pm.LastName == projectInputDTO.ProjectManagerName);

        if (customer == null)
            throw new Exception($"Customer '{projectInputDTO.CustomerName}' not found.");
        if (service == null)
            throw new Exception($"Service '{projectInputDTO.ServiceName}' not found.");
        if (projectManager == null)
            throw new Exception($"Project Manager '{projectInputDTO.ProjectManagerName}' not found.");

        int lastProjectId = await context.Projects.AnyAsync() ? await context.Projects.MaxAsync(p => p.ProjectId) : 0;
        string projectNumber = $"P-{1000 + lastProjectId + 1}";

        return new Project
        {
            ProjectNumber = projectNumber,
            Name = projectInputDTO.Name,
            Description = projectInputDTO.Description ?? "No description provided",
            StartDate = projectInputDTO.StartDate,
            EndDate = projectInputDTO.EndDate,
            StatusId = projectInputDTO.StatusId,
            CustomerId = customer.CustomerId,
            ServiceId = service.ServiceId,
            ProjectManagerId = projectManager.ProjectManagerId,
            TotalPrice = projectInputDTO.TotalPrice
        };
    }

    public static ProjectDTO CreateProjectDTO(Project project)
    {
        return new ProjectDTO
        {
            ProjectId = project.ProjectId,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            StatusId = project.StatusId,
            StatusName = project.Status?.StatusName,
            CustomerId = project.CustomerId,
            CustomerName = project.Customer?.CustomerName,
            ServiceId = project.ServiceId,
            ServiceName = project.Service?.ServiceName,
            ProjectManagerId = project.ProjectManagerId,
            ProjectManagerName = project.ProjectManager?.FirstName + " " + project.ProjectManager?.LastName,
            TotalPrice = project.TotalPrice
        };
    }
}
