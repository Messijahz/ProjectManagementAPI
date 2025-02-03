using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.Interfaces;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;
    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }
    public async Task<Project?> GetByIdAsync(int Id)
    {
        return await _context.Projects.FindAsync(Id);
    }
    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var project = await GetByIdAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        
    }
}

