using Data.Models;

namespace Data.Interfaces;


public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int Id);
    Task<IEnumerable<Project>> FindByNameAsync(string name);
    Task AddAsync(Project project);
    Task<bool> UpdateAsync(Project project);
    Task DeleteAsync(int id);
}