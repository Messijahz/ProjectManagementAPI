using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Data.Interfaces;
using Data.Factories;
using Data.DTOs;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.Extensions.Logging;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(IProjectRepository projectRepository, ApplicationDbContext context, ILogger<ProjectController> logger)
    {
        _projectRepository = projectRepository;
        _context = context;
        _logger = logger;
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
            return NotFound($"Projekt med ID {id} hittades inte.");
        }
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectInputDTO projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerName == projectDto.CustomerName);
            if (customer == null)
            {
                customer = new Customer
                {
                    CustomerName = projectDto.CustomerName,
                    ContactPerson = projectDto.ContactPerson
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var nameParts = projectDto.ProjectManagerName.Split(' ');
            var projectManager = await _context.ProjectManagers
                .FirstOrDefaultAsync(pm => pm.FirstName + " " + pm.LastName == projectDto.ProjectManagerName);
            if (projectManager == null)
            {
                projectManager = new ProjectManager
                {
                    FirstName = nameParts[0],
                    LastName = nameParts.Length > 1 ? nameParts[1] : "",
                    RoleId = 1,
                    Email = ""
                };
                _context.ProjectManagers.Add(projectManager);
                await _context.SaveChangesAsync();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceName == projectDto.ServiceName);
            if (service == null)
            {
                var serviceType = await _context.ServiceTypes
                    .FirstOrDefaultAsync(st => st.ServiceTypeName == projectDto.ServiceName);
                if (serviceType == null)
                {
                    serviceType = new ServiceType
                    {
                        ServiceTypeName = projectDto.ServiceName
                    };
                    _context.ServiceTypes.Add(serviceType);
                    await _context.SaveChangesAsync();
                }

                service = new Service
                {
                    ServiceName = projectDto.ServiceName,
                    ServiceTypeId = serviceType.ServiceTypeId,
                    PricePerUnit = projectDto.PricePerUnit,
                    UnitId = 1
                };
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
            }

            var project = await ProjectFactory.CreateProjectAsync(projectDto, _context);
            await _projectRepository.AddAsync(project);

            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
        }
        catch (DbUpdateException dbEx)
        {
            await transaction.RollbackAsync();
            _logger.LogError("Databasfel vid skapande av projekt: {Error}", dbEx.InnerException?.Message);
            return BadRequest(new { message = "Databasfel: " + dbEx.InnerException?.Message });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError("Transaction rollback triggered: {Error}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectInputDTO projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Projekt med ID {id} hittades inte.");
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerName == projectDto.CustomerName);
            if (customer == null)
            {
                customer = new Customer
                {
                    CustomerName = projectDto.CustomerName,
                    ContactPerson = projectDto.ContactPerson
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var nameParts = projectDto.ProjectManagerName.Split(' ');
            var projectManager = await _context.ProjectManagers
                .FirstOrDefaultAsync(pm => pm.FirstName + " " + pm.LastName == projectDto.ProjectManagerName);
            if (projectManager == null)
            {
                projectManager = new ProjectManager
                {
                    FirstName = nameParts[0],
                    LastName = nameParts.Length > 1 ? nameParts[1] : "",
                    RoleId = 1,
                    Email = ""
                };
                _context.ProjectManagers.Add(projectManager);
                await _context.SaveChangesAsync();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(s => s.ServiceName == projectDto.ServiceName);
            if (service == null)
            {
                var serviceType = await _context.ServiceTypes
                    .FirstOrDefaultAsync(st => st.ServiceTypeName == projectDto.ServiceName);
                if (serviceType == null)
                {
                    serviceType = new ServiceType
                    {
                        ServiceTypeName = projectDto.ServiceName
                    };
                    _context.ServiceTypes.Add(serviceType);
                    await _context.SaveChangesAsync();
                }

                service = new Service
                {
                    ServiceName = projectDto.ServiceName,
                    ServiceTypeId = serviceType.ServiceTypeId,
                    PricePerUnit = projectDto.PricePerUnit,
                    UnitId = 1
                };
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
            }

            project.Name = projectDto.Name;
            project.Description = projectDto.Description ?? project.Description;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.StatusId = projectDto.StatusId;
            project.CustomerId = customer.CustomerId;
            project.ServiceId = service.ServiceId;
            project.ProjectManagerId = projectManager.ProjectManagerId;
            project.TotalPrice = projectDto.TotalPrice;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return Ok(project);
        }
        catch (DbUpdateException dbEx)
        {
            await transaction.RollbackAsync();
            _logger.LogError("Databasfel vid uppdatering av projekt: {Error}", dbEx.InnerException?.Message);
            return BadRequest(new { message = "Databasfel: " + dbEx.InnerException?.Message });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError("Transaction rollback triggered vid uppdatering: {Error}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound($"Projekt med ID {id} hittades inte.");
        }

        await _projectRepository.DeleteAsync(id);
        return NoContent();
    }
}