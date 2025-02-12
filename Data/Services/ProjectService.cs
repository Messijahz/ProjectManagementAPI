using Data.DTOs;
using Data.Interfaces;
using Data.Factories;

namespace Data.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Select(ProjectFactory.CreateProjectDTO);
    }

    public async Task<ProjectDTO?> GetProjectByIdAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return project != null ? ProjectFactory.CreateProjectDTO(project) : null;
    }

    public async Task<bool> CustomerHasProjectsAsync(int customerId)
    {
        var projects = await _projectRepository.GetAllAsync();
        return projects.Any(p => p.CustomerId == customerId);
    }

    public async Task<bool> AddProjectAsync(ProjectInputDTO projectInputDTO)
    {
        var project = ProjectFactory.CreateProject(projectInputDTO);
        await _projectRepository.AddAsync(project);
        return true;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectInputDTO projectInputDTO)
    {
        var existingProject = await _projectRepository.GetByIdAsync(id);
        if (existingProject == null)
            return false;

        existingProject.Name = projectInputDTO.Name;
        existingProject.Description = projectInputDTO.Description!;
        existingProject.StartDate = projectInputDTO.StartDate;
        existingProject.EndDate = projectInputDTO.EndDate;
        existingProject.StatusId = projectInputDTO.StatusId;
        existingProject.CustomerId = projectInputDTO.CustomerId;
        existingProject.ServiceId = projectInputDTO.ServiceId;
        existingProject.ProjectManagerId = projectInputDTO.ProjectManagerId;
        existingProject.TotalPrice = projectInputDTO.TotalPrice;

        return await _projectRepository.UpdateAsync(existingProject);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        await _projectRepository.DeleteAsync(id);
        return true;
    }
}
