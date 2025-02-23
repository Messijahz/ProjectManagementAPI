using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Data.Models;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectManagerRepository : BaseRepository<ProjectManager>, IProjectManagerRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectManagerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public override async Task<IEnumerable<ProjectManager>> GetAllAsync()
    {
        return await _context.ProjectManagers.ToListAsync();
    }

    public override async Task<ProjectManager?> GetByIdAsync(int id)
    {
        return await _context.ProjectManagers.FirstOrDefaultAsync(pm => pm.ProjectManagerId == id);
    }

    public override async Task<IEnumerable<ProjectManager>> FindAsync(Expression<Func<ProjectManager, bool>> predicate)
    {
        return await _context.ProjectManagers.Where(predicate).ToListAsync();
    }

    public override async Task AddAsync(ProjectManager projectManager)
    {
        if (projectManager == null)
        {
            throw new ArgumentNullException(nameof(projectManager), "Project Manager cannot be null.");
        }

        await _context.ProjectManagers.AddAsync(projectManager);
        await _context.SaveChangesAsync();
    }

    public override async Task<bool> UpdateAsync(ProjectManager projectManager)
    {
        if (projectManager == null)
        {
            throw new ArgumentNullException(nameof(projectManager), "Project Manager cannot be null.");
        }

        var existingProjectManager = await _context.ProjectManagers.FindAsync(projectManager.ProjectManagerId);
        if (existingProjectManager == null)
        {
            return false;
        }

        _context.Entry(existingProjectManager).CurrentValues.SetValues(projectManager);
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task DeleteAsync(int id)
    {
        var projectManager = await GetByIdAsync(id);
        if (projectManager == null)
        {
            throw new KeyNotFoundException($"Project Manager with ID {id} not found.");
        }

        _context.ProjectManagers.Remove(projectManager);
        await _context.SaveChangesAsync();
    }
}