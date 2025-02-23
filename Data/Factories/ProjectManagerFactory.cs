using Data.DTOs;
using Data.Models;

namespace Data.Factories;

public static class ProjectManagerFactory
{
    public static ProjectManagerDTO CreateProjectManagerDTO(ProjectManager projectManager)
    {
        return new ProjectManagerDTO
        {
            ProjectManagerId = projectManager.ProjectManagerId,
            FirstName = projectManager.FirstName,
            LastName = projectManager.LastName,
            Email = projectManager.Email,
            RoleId = projectManager.RoleId
        };
    }

    public static ProjectManager CreateProjectManager(ProjectManagerInputDTO projectManagerDto)
    {
        return new ProjectManager
        {
            FirstName = projectManagerDto.FirstName,
            LastName = projectManagerDto.LastName,
            Email = projectManagerDto.Email,
            RoleId = projectManagerDto.RoleId
        };
    }
}