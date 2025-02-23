using Data.DTOs;
using Data.Interfaces;
using Data.Factories;

namespace Data.Services;

public class ProjectManagerService : IProjectManagerService
{
    private readonly IProjectManagerRepository _projectManagerRepository;

    public ProjectManagerService(IProjectManagerRepository projectManagerRepository)
    {
        _projectManagerRepository = projectManagerRepository;
    }

    public async Task<IEnumerable<ProjectManagerDTO>> GetAllProjectManagersAsync()
    {
        var projectManagers = await _projectManagerRepository.GetAllAsync();
        return projectManagers.Select(ProjectManagerFactory.CreateProjectManagerDTO);
    }

    public async Task<ProjectManagerDTO?> GetProjectManagerByIdAsync(int id)
    {
        var projectManager = await _projectManagerRepository.GetByIdAsync(id);
        return projectManager != null ? ProjectManagerFactory.CreateProjectManagerDTO(projectManager) : null;
    }

    public async Task<bool> AddProjectManagerAsync(ProjectManagerInputDTO projectManagerInputDTO)
    {
        var projectManager = ProjectManagerFactory.CreateProjectManager(projectManagerInputDTO);
        await _projectManagerRepository.AddAsync(projectManager);
        return true;
    }

    public async Task<bool> UpdateProjectManagerAsync(int id, ProjectManagerInputDTO projectManagerInputDTO)
    {
        var existingProjectManager = await _projectManagerRepository.GetByIdAsync(id);
        if (existingProjectManager == null)
            return false;

        existingProjectManager.FirstName = projectManagerInputDTO.FirstName;
        existingProjectManager.LastName = projectManagerInputDTO.LastName;
        existingProjectManager.Email = projectManagerInputDTO.Email;
        existingProjectManager.RoleId = projectManagerInputDTO.RoleId;

        await _projectManagerRepository.UpdateAsync(existingProjectManager);
        return true;
    }

    public async Task<bool> DeleteProjectManagerAsync(int id)
    {
        await _projectManagerRepository.DeleteAsync(id);
        return true;
    }
}