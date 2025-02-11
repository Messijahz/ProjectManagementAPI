using Data.Models;
using Data.DTOs;

namespace Data.Factories;

public static class ProjectFactory
{
    public static Project CreateProject(ProjectInputDTO projectDto)
    {
        return new Project
        {
            Name = projectDto.Name,
            Description = projectDto.Description!,
            StartDate = projectDto.StartDate,
            EndDate = projectDto.EndDate,
            StatusId = projectDto.StatusId,
            CustomerId = projectDto.CustomerId,
            ServiceId = projectDto.ServiceId,
            ProjectManagerId = projectDto.ProjectManagerId,
            TotalPrice = projectDto.TotalPrice
        };
    }
}
