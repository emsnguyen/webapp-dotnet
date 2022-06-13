namespace WebApp.Models;
public class ProjectUser : Auditable
{
    public Project? Project { get; set;}
    public User? User { get; set; }
    public long ProjectId { get; set; }
    public long UserId { get; set; }
    public int ProjectRoleId { get; set; }
}