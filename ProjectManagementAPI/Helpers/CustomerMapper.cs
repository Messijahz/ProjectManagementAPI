using ProjectManagementAPI.DTOs;
using Data.Models;

namespace ProjectManagementAPI.Helpers;

public static class CustomerMapper
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

    public static Customer ToEntity(CustomerInputDTO customerInputDTO)
    {
        return new Customer
        {
            CustomerName = customerInputDTO.CustomerName,
            ContactPerson = customerInputDTO.ContactPerson
        };
    }
}
