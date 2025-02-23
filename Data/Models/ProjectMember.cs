using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;


public class ProjectMember
{
    [ForeignKey("Project")]
    public required int ProjectId { get; set; }
    public required Project Project { get; set; }

    [ForeignKey("ProjectManager")]
    public required int ProjectManagerId { get; set; }
    public required ProjectManager ProjectManager { get; set; }
}