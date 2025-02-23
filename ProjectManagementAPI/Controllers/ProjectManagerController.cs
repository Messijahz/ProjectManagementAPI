using Microsoft.AspNetCore.Mvc;
using Data.Interfaces;
using Data.DTOs;
using Data.Factories;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectManagerController : ControllerBase
{
    private readonly IProjectManagerRepository _projectManagerRepository;

    public ProjectManagerController(IProjectManagerRepository projectManagerRepository)
    {
        _projectManagerRepository = projectManagerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjectManagers()
    {
        var projectManagers = await _projectManagerRepository.GetAllAsync();
        return Ok(projectManagers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectManagerById(int id)
    {
        var projectManager = await _projectManagerRepository.GetByIdAsync(id);
        if (projectManager == null)
        {
            return NotFound($"Project Manager with ID {id} not found.");
        }
        return Ok(projectManager);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectManager([FromBody] ProjectManagerInputDTO projectManagerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var projectManager = ProjectManagerFactory.CreateProjectManager(projectManagerDto);
        await _projectManagerRepository.AddAsync(projectManager);
        return CreatedAtAction(nameof(GetProjectManagerById), new { id = projectManager.ProjectManagerId }, projectManager);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectManager(int id, [FromBody] ProjectManagerInputDTO projectManagerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProjectManager = await _projectManagerRepository.GetByIdAsync(id);
        if (existingProjectManager == null)
        {
            return NotFound($"Project Manager with ID {id} not found.");
        }

        existingProjectManager.FirstName = projectManagerDto.FirstName;
        existingProjectManager.LastName = projectManagerDto.LastName;
        existingProjectManager.Email = projectManagerDto.Email;
        existingProjectManager.RoleId = projectManagerDto.RoleId;

        await _projectManagerRepository.UpdateAsync(existingProjectManager);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectManager(int id)
    {
        var projectManager = await _projectManagerRepository.GetByIdAsync(id);
        if (projectManager == null)
        {
            return NotFound($"Project Manager with ID {id} not found.");
        }

        await _projectManagerRepository.DeleteAsync(id);
        return NoContent();
    }
}