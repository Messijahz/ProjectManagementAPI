using ProjectManagementAPI.DTOs;
using ProjectManagementAPI.Models;

namespace ProjectManagement.BusinessLayer.Factories;

public static class CustomerFactory
{
    public static CustomerDTO ToDTO(Customer customer)
    {
        return new CustomerDTO
        {
            CustomerId = customer.CustomerId,
            CustomerName = customer.CustomerName,
            ContactPerson = customer.ContactPerson
        };
    }

    public static Customer FromDTO(CustomerInputDTO dto)
    {
        return new Customer
        {
            CustomerName = dto.CustomerName,
            ContactPerson = dto.ContactPerson ?? string.Empty
        };
    }
}
