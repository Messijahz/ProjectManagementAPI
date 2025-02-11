using System.ComponentModel.DataAnnotations;

namespace Data.Models;


public class Role
{
    [Key]
    public int RoleId { get; set; }


    [MaxLength(100)]
    public string? RoleName { get; set; }
}
