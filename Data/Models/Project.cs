using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;


public class Project
{
    [Key]
    public int ProjectId { get; set; }


    [MaxLength(100)]
    public required string Name { get; set; }


    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }


    [ForeignKey("Status")]
    public required int StatusId { get; set; }
    public Status? Status { get; set; }

    [ForeignKey("Customer")]
    public required int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [ForeignKey("Service")]
    public required int ServiceId { get; set; }
    public Service? Service { get; set; }

    [ForeignKey("ProjectManager")]
    public required int ProjectManagerId { get; set; }
    public ProjectManager? ProjectManager { get; set; }


    [Column(TypeName = "decimal(10, 2)")]
    public required decimal TotalPrice { get; set; }
}
