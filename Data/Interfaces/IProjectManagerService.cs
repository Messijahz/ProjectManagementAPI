using Data.DTOs;

namespace Data.Interfaces;

public interface IProjectManagerService
{
    Task<IEnumerable<ProjectManagerDTO>> GetAllProjectManagersAsync();
    Task<ProjectManagerDTO?> GetProjectManagerByIdAsync(int id);
    Task<bool> AddProjectManagerAsync(ProjectManagerInputDTO projectManagerInputDTO);
    Task<bool> UpdateProjectManagerAsync(int id, ProjectManagerInputDTO projectManagerInputDTO);
    Task<bool> DeleteProjectManagerAsync(int id);
}