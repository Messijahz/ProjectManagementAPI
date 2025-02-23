using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;


public class ProjectManager
{
    [Key]
    public int ProjectManagerId { get; set; }


    [MaxLength(100)]
    public required string FirstName { get; set; }


    [MaxLength(100)]
    public required string LastName { get; set; }


    [MaxLength(100)]
    public required string Email { get; set; }

    [ForeignKey("Role")]
    public required int RoleId { get; set; }
    public Role? Role { get; set; }
}