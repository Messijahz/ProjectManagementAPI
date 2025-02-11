using Microsoft.Extensions.Logging;
using ProjectManagement.BusinessLayer.Factories;


namespace ProjectManagement.BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                return customers.Select(CustomerFactory.ToDTO).ToList();

        }
        catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all customers.");
                throw new ApplicationException("Could not retrieve customers.", ex);
            }
        }

        public async Task<CustomerDTO?> GetCustomerByCustomerNameAsync(string customerName)
        {
            try
            {
                var customer = await _customerRepository.FindAsync(c => c.CustomerName == customerName);
                return customer.FirstOrDefault() == null ? null : CustomerFactory.ToDTO(customer.FirstOrDefault()!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching customer with name {customerName}.");
                throw new ApplicationException($"Could not retrieve the customer with name {customerName}.", ex);
            }
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                return customer == null ? null : CustomerFactory.ToDTO(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching customer with ID {id}.");
                throw new ApplicationException($"Could not retrieve the customer with ID {id}.", ex);
            }
        }

        public async Task<bool> AddCustomerAsync(CustomerInputDTO customerInputDTO)
        {
            try
            {
            var customer = CustomerFactory.FromDTO(customerInputDTO);

            await _customerRepository.AddAsync(customer);
                _logger.LogInformation($"Customer '{customer.CustomerName}' successfully added.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a customer.");
                throw new ApplicationException("Could not add the customer.", ex);
            }
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerInputDTO customerInputDTO)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    _logger.LogWarning($"Customer with ID {id} not found.");
                    return false;
                }

                existingCustomer.CustomerName = customerInputDTO.CustomerName;
                existingCustomer.ContactPerson = customerInputDTO.ContactPerson ?? string.Empty;

                await _customerRepository.UpdateAsync(existingCustomer);
                _logger.LogInformation($"Customer with ID {id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating customer with ID {id}.");
                throw new ApplicationException($"Could not update the customer with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    _logger.LogWarning($"Customer with ID {id} not found.");
                    return false;
                }

                await _customerRepository.DeleteAsync(id);
                _logger.LogInformation($"Customer with ID {id} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting customer with ID {id}.");
                throw new ApplicationException($"Could not delete the customer with ID {id}.", ex);
            }
        }
    }
}
