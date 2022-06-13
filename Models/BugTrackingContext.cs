using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class BugTrackingContext : DbContext
{
    public DbSet<Issue>? Issues { get; set; }
    public DbSet<Project>? Projects { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<IssueUpdateHistory>? IssueUpdateHistories { get; set; }
    public DbSet<Comment>? Comments { get; set; }

    public string DbPath { get; }

    public BugTrackingContext(DbContextOptions<BugTrackingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Primary key
        modelBuilder.Entity<Issue>()
        .HasKey(i => new { i.IssueId });
        modelBuilder.Entity<Project>()
        .HasKey(p => new { p.ProjectId });
        modelBuilder.Entity<Category>()
        .HasKey(p => new { p.CategoryId });
        modelBuilder.Entity<Comment>()
        .HasKey(p => new { p.CommentId });
        modelBuilder.Entity<User>()
        .HasKey(p => new { p.UserId });
        modelBuilder.Entity<IssueUpdateHistory>()
        .HasKey(p => new { p.IssueUpdateHistoryId });
        modelBuilder.Entity<ProjectUser>()
        .HasKey(p => new { p.ProjectId, p.UserId });

        // Many to many relationship
        modelBuilder.Entity<ProjectUser>()
            .HasOne(pu => pu.Project)
            .WithMany(b => b.ProjectUsers)
            .HasForeignKey(pu => pu.ProjectId);
        modelBuilder.Entity<ProjectUser>()
            .HasOne(pu => pu.User)
            .WithMany(c => c.ProjectUsers)
            .HasForeignKey(pu => pu.UserId);

        // One to many relationship
        modelBuilder.Entity<Issue>()
            .HasMany(c => c.Comments)
            .WithOne(i => i.Issue)
            .HasForeignKey(i => i.IssueId);
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Issues)
            .WithOne(i => i.Category);
        modelBuilder.Entity<Project>()
            .HasMany(c => c.Issues)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId);
        modelBuilder.Entity<Issue>()
            .HasMany(c => c.IssueUpdateHistories)
            .WithOne(i => i.Issue)
            .HasForeignKey(i => i.IssueUpdateHistoryId);

        modelBuilder.Entity<Issue>()
                    .HasOne(m => m.AssignedTo)
                    .WithMany(t => t.AssignedToIssues)
                    .HasForeignKey(m => m.AssignedToId);

        modelBuilder.Entity<Issue>()
                    .HasOne(m => m.CreatedBy)
                    .WithMany(t => t.OwnedIssues)
                    .HasForeignKey(m => m.CreatedById);

        modelBuilder.Entity<Project>().HasData(
            new Project { ProjectId = 1, Name = "Test Project", Description = "First project" });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = this.ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);
        foreach (var insertedEntry in insertedEntries)
        {
            var auditableEntity = insertedEntry as Auditable;
            //If the inserted object is an Auditable. 
            if (auditableEntity != null)
            {
                auditableEntity.CreatedAt = DateTimeOffset.UtcNow;
                auditableEntity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
        var modifiedEntries = this.ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Modified)
                   .Select(x => x.Entity);
        foreach (var modifiedEntry in modifiedEntries)
        {
            //If the inserted object is an Auditable. 
            var auditableEntity = modifiedEntry as Auditable;
            if (auditableEntity != null)
            {
                auditableEntity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}