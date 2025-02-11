using ProjectManagement.BusinessLayer.Factories;

namespace ProjectManagement.BusinessLayer.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync()
    {
        try
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(ProjectFactory.ToDTO).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all projects.");
            throw new ApplicationException("Could not retrieve projects.", ex);
        }
    }

    public async Task<ProjectDTO?> GetProjectByIdAsync(int id)
    {
        try
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return project == null ? null : ProjectFactory.ToDTO(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching project with ID {id}.");
            throw new ApplicationException($"Could not retrieve the project with ID {id}.", ex);
        }
    }

    public async Task<bool> CustomerHasProjectsAsync(int customerId)
    {
        try
        {
            var projects = await _projectRepository.FindAsync(p => p.CustomerId == customerId);
            return projects.Any();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while checking projects for customer with ID {customerId}.");
            throw new ApplicationException($"Could not check projects for customer with ID {customerId}.", ex);
        }
    }


    public async Task<bool> AddProjectAsync(ProjectInputDTO projectInputDTO)
    {
        try
        {
            var project = ProjectFactory.FromDTO(projectInputDTO);

            await _projectRepository.AddAsync(project);
            _logger.LogInformation("Project added successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a project.");
            throw new ApplicationException("Could not add the project.", ex);
        }
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectInputDTO projectInputDTO)
    {
        try
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
            {
                _logger.LogWarning($"Project with ID {id} not found.");
                return false;
            }

            existingProject.Name = projectInputDTO.Name;
            existingProject.Description = projectInputDTO.Description ?? string.Empty;
            existingProject.StartDate = projectInputDTO.StartDate;
            existingProject.EndDate = projectInputDTO.EndDate;
            existingProject.StatusId = projectInputDTO.StatusId;
            existingProject.CustomerId = projectInputDTO.CustomerId;
            existingProject.ServiceId = projectInputDTO.ServiceId;
            existingProject.ProjectManagerId = projectInputDTO.ProjectManagerId;
            existingProject.TotalPrice = projectInputDTO.TotalPrice;

            await _projectRepository.UpdateAsync(existingProject);
            _logger.LogInformation($"Project with ID {id} updated successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating project with ID {id}.");
            throw new ApplicationException($"Could not update the project with ID {id}.", ex);
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                _logger.LogWarning($"Project with ID {id} not found.");
                return false;
            }

            await _projectRepository.DeleteAsync(id);
            _logger.LogInformation($"Project with ID {id} deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting project with ID {id}.");
            throw new ApplicationException($"Could not delete the project with ID {id}.", ex);
        }
    }
}
