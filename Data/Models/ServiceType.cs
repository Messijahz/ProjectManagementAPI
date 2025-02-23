using System.ComponentModel.DataAnnotations;

namespace Data.Models;


public class ServiceType
{
    [Key]
    public int ServiceTypeId { get; set; }


    [MaxLength(100)]
    public required string ServiceTypeName { get; set; }
}