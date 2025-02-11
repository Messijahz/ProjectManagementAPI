using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }


    [MaxLength(100)]
    public required string CustomerName { get; set; } = string.Empty;


    [MaxLength(100)]
    public required string ContactPerson { get; set; }
}
