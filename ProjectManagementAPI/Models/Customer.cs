using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }


    [MaxLength(100)]
    public required string Name { get; set; }


    [MaxLength(100)]
    public required string ContactPerson { get; set; }
}
