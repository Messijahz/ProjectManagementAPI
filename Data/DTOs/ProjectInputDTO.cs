namespace Data.DTOs;

public class ProjectInputDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }
    public int CustomerId { get; set; }
    public int ServiceId { get; set; }
    public int ProjectManagerId { get; set; }
    public decimal TotalPrice { get; set; }
}
