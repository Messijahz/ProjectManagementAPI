using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Data.Repositories;
using Data.Interfaces;
using ProjectManagementAPI.DTOs;

namespace ProjectManagementAPI.Controllers;

public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
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


    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _projectRepository.AddAsync(project);
        return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
    {
        if (id != project.ProjectId)
        {
            return BadRequest("Project ID mismatch.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProject = await _projectRepository.GetByIdAsync(id);
        if (existingProject == null)
        {
            return NotFound($"Project with ID {id} not found.");
        }

        await _projectRepository.UpdateAsync(project);
        return NoContent();
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
