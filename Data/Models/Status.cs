using System.ComponentModel.DataAnnotations;

namespace Data.Models;


public class Status
{
    [Key]
    public int StatusId { get; set; }


    [MaxLength(50)]
    public required string StatusName { get; set; }
}
