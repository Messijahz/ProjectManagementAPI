using Data.DTOs;

namespace Data.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
    Task<CustomerDTO?> GetCustomerByIdAsync(int id);
    Task<CustomerDTO?> GetCustomerByCustomerNameAsync(string customerName);
    Task<bool> AddCustomerAsync(CustomerInputDTO customerInputDTO);
    Task<bool> UpdateCustomerAsync(int id, CustomerInputDTO customerInputDTO);
    Task<bool> DeleteCustomerAsync(int id);
}
