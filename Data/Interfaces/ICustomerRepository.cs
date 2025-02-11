using Data.Models;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate);
    Task AddAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task DeleteAsync(int id);
}
