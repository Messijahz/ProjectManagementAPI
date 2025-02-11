using ProjectManagementAPI.DTOs;
using ProjectManagementAPI.Models;

namespace ProjectManagement.BusinessLayer.Factories;

public static class ProjectFactory
{
    public static ProjectDTO ToDTO(Project project)
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

    public static Project FromDTO(ProjectInputDTO dto)
    {
        return new Project
        {
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            StatusId = dto.StatusId,
            CustomerId = dto.CustomerId,
            ServiceId = dto.ServiceId,
            ProjectManagerId = dto.ProjectManagerId,
            TotalPrice = dto.TotalPrice
        };
    }
}
