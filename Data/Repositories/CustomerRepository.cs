using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Data.Models;
using System.Linq.Expressions;


namespace Data.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public override async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public override async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
    {
        return await _context.Customers.Where(predicate).ToListAsync();
    }

    public override async Task AddAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
        }

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public override async Task<bool> UpdateAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
        }

        var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
        if (existingCustomer == null)
        {
            return false;
        }

        _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task DeleteAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}
