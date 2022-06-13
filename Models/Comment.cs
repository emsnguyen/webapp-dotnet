namespace WebApp.Models;
public class Comment : Auditable
{
    public long CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public long IssueId { get; set; }
    public Issue? Issue { get; set; }
}