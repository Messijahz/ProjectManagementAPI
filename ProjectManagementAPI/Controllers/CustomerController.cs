using Microsoft.AspNetCore.Mvc;
using Data.Interfaces;
using Data.Factories;
using Data.DTOs;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerRepository.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerInputDTO customerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = CustomerFactory.CreateCustomer(customerDto);
        await _customerRepository.AddAsync(customer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerInputDTO customerDto)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(id);
        if (existingCustomer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        existingCustomer.CustomerName = customerDto.CustomerName;
        existingCustomer.ContactPerson = customerDto.ContactPerson;

        await _customerRepository.UpdateAsync(existingCustomer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        await _customerRepository.DeleteAsync(id);
        return NoContent();
    }
}