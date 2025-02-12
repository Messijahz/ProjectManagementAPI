using Data.DTOs;
using Data.Models;

namespace Data.Factories;

public static class ProjectFactory
{
    public static Project CreateProject(ProjectInputDTO projectInputDTO)
    {
        return new Project
        {
            Name = projectInputDTO.Name,
            Description = projectInputDTO.Description!,
            StartDate = projectInputDTO.StartDate,
            EndDate = projectInputDTO.EndDate,
            StatusId = projectInputDTO.StatusId,
            CustomerId = projectInputDTO.CustomerId,
            ServiceId = projectInputDTO.ServiceId,
            ProjectManagerId = projectInputDTO.ProjectManagerId,
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
            EndDate = project.EndDate ?? DateTime.MinValue,
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
