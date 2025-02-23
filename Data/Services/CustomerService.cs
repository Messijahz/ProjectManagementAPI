using Data.DTOs;
using Data.Interfaces;
using Data.Factories;


namespace Data.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(CustomerFactory.CreateCustomerDTO);
    }

    public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer != null ? CustomerFactory.CreateCustomerDTO(customer) : null;
    }

    public async Task<CustomerDTO?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customers = await _customerRepository.FindAsync(c => c.CustomerName == customerName);
        var customer = customers.FirstOrDefault();
        return customer != null ? CustomerFactory.CreateCustomerDTO(customer) : null;
    }

    public async Task<bool> AddCustomerAsync(CustomerInputDTO customerInputDTO)
    {
        var customer = CustomerFactory.CreateCustomer(customerInputDTO);
        await _customerRepository.AddAsync(customer);
        return true;
    }

    public async Task<bool> UpdateCustomerAsync(int id, CustomerInputDTO customerInputDTO)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(id);
        if (existingCustomer == null)
            return false;

        existingCustomer.CustomerName = customerInputDTO.CustomerName;
        existingCustomer.ContactPerson = customerInputDTO.ContactPerson;

        return await _customerRepository.UpdateAsync(existingCustomer);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        await _customerRepository.DeleteAsync(id);
        return true;
    }
}