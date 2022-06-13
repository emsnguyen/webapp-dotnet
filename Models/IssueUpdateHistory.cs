namespace WebApp.Models;
public class IssueUpdateHistory : Auditable
{
    public long IssueUpdateHistoryId { get; set; }
    public long IssueId { get; set; }
    public Issue? Issue { get; set; }
    public long UpdatedBy { get; set; }
}