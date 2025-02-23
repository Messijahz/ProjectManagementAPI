namespace Data.DTOs;

public class ProjectDTO
{
    public int ProjectId { get; set; }

    public string ProjectNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int StatusId { get; set; }
    public string? StatusName { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public int ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public int ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; }
    public decimal TotalPrice { get; set; }
}