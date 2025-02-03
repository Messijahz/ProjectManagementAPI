using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int Id);
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int id);
}
