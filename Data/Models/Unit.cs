using System.ComponentModel.DataAnnotations;

namespace Data.Models;


public class Unit
{
    [Key]
    public int UnitId { get; set; }


    [MaxLength(50)]
    public required string UnitName { get; set; }
}
