namespace WebApp.Models;
public class Project : Auditable
{
    public long ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Issue>? Issues { get; set; }
    public ICollection<ProjectUser>? ProjectUsers { get; set; }
    public override string ToString()
    {
        return String.Format("Id: {0}, Name: {1}, Description: {2}" , ProjectId, Name, Description); 
    }
}