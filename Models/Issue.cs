using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class Issue : Auditable
{
    public long IssueId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Status { get; set; }
    public int CategoryId { get; set; }
    public long ProjectId { get; set; }
    public long CreatedById { get; set; }
    public long AssignedToId { get; set; }
    public DateTime DueDate { get; set; }
    public Category? Category { get; set; }
    public Project? Project { get; set; }
    public virtual User? AssignedTo { get; set; }
    public virtual User? CreatedBy { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<IssueUpdateHistory>? IssueUpdateHistories { get; set; }
}