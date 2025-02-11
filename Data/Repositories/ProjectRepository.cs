using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;
    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
        .Include(p => p.Status)
        .Include(p => p.Customer)
        .Include(p => p.Service)
            .ThenInclude(s => s!.ServiceType)
        .Include(p => p.Service)
            .ThenInclude(s => s!.Unit)
        .Include(p => p.ProjectManager)
            .ThenInclude(pm => pm!.Role)
        .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects
        .Include(p => p.Status)
        .Include(p => p.Customer)
        .Include(p => p.Service)
            .ThenInclude(s => s!.ServiceType)
        .Include(p => p.Service)
            .ThenInclude(s => s!.Unit)
        .Include(p => p.ProjectManager)
            .ThenInclude(pm => pm!.Role)
        .FirstOrDefaultAsync(p => p.ProjectId == id);
    }

    public async Task<IEnumerable<Project>> FindByNameAsync(string name)
    {
        return await _context.Projects
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }
    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> UpdateAsync(Project project)
    {
        var existingProject = await _context.Projects.FindAsync(project.ProjectId);
        if (existingProject == null)
        {
            return false;
        }

        _context.Entry(existingProject).CurrentValues.SetValues(project);
        await _context.SaveChangesAsync();
        return true;
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

