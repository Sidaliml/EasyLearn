using Azure;
using EasyLearn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Infrastructure.Data;

public class EasyLearnDbContext : DbContext
{
    public EasyLearnDbContext(DbContextOptions<EasyLearnDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<AssignmentTag> AssignmentTags => Set<AssignmentTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AssignmentTag>()
            .HasKey(x => new { x.AssignmentId, x.TagId });

        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Assignments)
            .WithOne(a => a.Subject!)
            .HasForeignKey(a => a.SubjectId);
    }
}
