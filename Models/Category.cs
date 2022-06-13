namespace WebApp.Models;
public class Category : Auditable
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Issue>? Issues { get; set; }
}