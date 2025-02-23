namespace Data.DTOs;

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
}