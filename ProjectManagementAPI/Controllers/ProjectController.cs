using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Data.Interfaces;
using Data.Factories;
using Data.DTOs;
using Microsoft.EntityFrameworkCore;
using Data;


namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    private readonly ApplicationDbContext _context;

    public ProjectController(IProjectRepository projectRepository, ApplicationDbContext context)
    {
        _projectRepository = projectRepository;
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectRepository.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound($"Project with ID {id} not found.");
        }
        return Ok(project);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> FindProjectByName(string name)
    {
        var projects = await _projectRepository.FindByNameAsync(name);

        if (projects == null || !projects.Any())
        {
            return NotFound($"No projects found with name: {name}");
        }

        return Ok(projects);
    }


    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectInputDTO projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var project = await ProjectFactory.CreateProjectAsync(projectDto, _context);
            await _projectRepository.AddAsync(project);

            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
        }
        catch (DbUpdateException dbEx)
        {
            return BadRequest(new { message = "Database error: " + dbEx.InnerException?.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message});
        }


    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectInputDTO projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProject = await _projectRepository.GetByIdAsync(id);
        if (existingProject == null)
        {
            return NotFound($"Project with ID {id} not found.");
        }

        try
        {
            existingProject.Name = projectDto.Name;
            existingProject.Description = projectDto.Description ?? existingProject.Description;
            existingProject.StartDate = projectDto.StartDate;
            existingProject.EndDate = projectDto.EndDate;
            existingProject.StatusId = projectDto.StatusId;
            existingProject.TotalPrice = projectDto.TotalPrice;

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerName == projectDto.CustomerName);
            if (customer == null)
            {
                return BadRequest($"Customer '{projectDto.CustomerName}' does not exist.");
            }
            existingProject.CustomerId = customer.CustomerId;

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceName == projectDto.ServiceName);
            if (service == null)
            {
                return BadRequest($"Service '{projectDto.ServiceName}' does not exist.");
            }
            existingProject.ServiceId = service.ServiceId;

            // Här tar jag och delar upp projektledarens namn i två delar i en lista och kollar om det finns en projektledare med dessa namn.
            var nameParts = projectDto.ProjectManagerName.Split(' ');
            // Om namnet inte innehåller både för och efternamn så returnerar jag en BadRequest.
            if (nameParts.Length < 2)
            {
                return BadRequest($"Invalid Project Manager name format: {projectDto.ProjectManagerName}. Use 'First Last'.");
            }
            // Annars kollar jag om det finns en projektledare med dessa namn.
            var projectManager = await _context.ProjectManagers
                .FirstOrDefaultAsync(pm => pm.FirstName == nameParts[0] && pm.LastName == nameParts[1]);
            if (projectManager == null)
            {
                return BadRequest($"Project Manager '{projectDto.ProjectManagerName}' does not exist.");
            }
            existingProject.ProjectManagerId = projectManager.ProjectManagerId;

            await _projectRepository.UpdateAsync(existingProject);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            await _projectRepository.DeleteAsync(id);
            return NoContent();
        }
}
