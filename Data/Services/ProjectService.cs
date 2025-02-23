using Data.DTOs;
using Data.Interfaces;
using Data.Factories;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ApplicationDbContext _context;

    public ProjectService(IProjectRepository projectRepository, ApplicationDbContext context)
    {
        _projectRepository = projectRepository;
        _context = context;
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
        var project = await ProjectFactory.CreateProjectAsync(projectInputDTO, _context);
        await _projectRepository.AddAsync(project);
        return true;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectInputDTO projectInputDTO)
    {
        var existingProject = await _projectRepository.GetByIdAsync(id);
        if (existingProject == null)
            return false;

        //Här hämtar jag in IDn baserat på namn.
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == projectInputDTO.CustomerName);
        var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceName == projectInputDTO.ServiceName);
        var projectManager = await _context.ProjectManagers.FirstOrDefaultAsync(pm => pm.FirstName + " " + pm.LastName == projectInputDTO.ProjectManagerName);

        if (customer == null || service == null || projectManager == null)
            return false;

        existingProject.Name = projectInputDTO.Name;
        existingProject.Description = projectInputDTO.Description!;
        existingProject.StartDate = projectInputDTO.StartDate;
        existingProject.EndDate = projectInputDTO.EndDate;
        existingProject.StatusId = projectInputDTO.StatusId;
        existingProject.CustomerId = customer.CustomerId;
        existingProject.ServiceId = service.ServiceId;
        existingProject.ProjectManagerId = projectManager.ProjectManagerId;
        existingProject.TotalPrice = projectInputDTO.TotalPrice;

        return await _projectRepository.UpdateAsync(existingProject);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        await _projectRepository.DeleteAsync(id);
        return true;
    }
}