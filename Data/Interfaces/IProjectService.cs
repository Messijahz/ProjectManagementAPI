using Data.DTOs;

namespace Data.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync();
    Task<ProjectDTO?> GetProjectByIdAsync(int id);
    Task<bool> CustomerHasProjectsAsync(int customerId);
    Task<bool> AddProjectAsync(ProjectInputDTO projectInputDTO);
    Task<bool> UpdateProjectAsync(int id, ProjectInputDTO projectInputDTO);
    Task<bool> DeleteProjectAsync(int id);
}
