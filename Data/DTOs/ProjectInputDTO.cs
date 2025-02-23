namespace Data.DTOs;

public class ProjectInputDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int StatusId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ContactPerson {  get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal PricePerUnit { get; set; }
    public string ProjectManagerName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
}