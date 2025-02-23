using Data.DTOs;
using Data.Models;

namespace Data.Factories;

public static class CustomerFactory
{
    public static CustomerDTO CreateCustomerDTO(Customer customer)
    {
        return new CustomerDTO
        {
            CustomerId = customer.CustomerId,
            CustomerName = customer.CustomerName,
            ContactPerson = customer.ContactPerson
        };
    }

    public static Customer CreateCustomer(CustomerInputDTO customerDto)
    {
        return new Customer
        {
            CustomerName = customerDto.CustomerName,
            ContactPerson = customerDto.ContactPerson
        };
    }
}