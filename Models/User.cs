namespace WebApp.Models;

public class User : Auditable
{
    public long UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<Comment>? Comments { get;set; }
    public ICollection<Project>? Projects { get;set; }
    public virtual ICollection<Issue>? OwnedIssues { get;set; }
    public virtual ICollection<Issue>? AssignedToIssues { get;set; }
    public ICollection<ProjectUser>? ProjectUsers {get;set;}
}