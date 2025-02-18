using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;


public class Service
{
    [Key]
    public int ServiceId { get; set; }


    [MaxLength(100)]
    public required string ServiceName { get; set; }


    [Column(TypeName = "decimal(10, 2)")]
    public required decimal PricePerUnit { get; set; }

    [ForeignKey("ServiceType")]
    public required int ServiceTypeId { get; set; }
    public ServiceType? ServiceType { get; set; }

    [ForeignKey("Unit")]
    public required int UnitId { get; set; }
    public Unit? Unit { get; set; }
}
