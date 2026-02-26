namespace EasyLearn.Domain.Entities;

public class Assignment
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }

    // One-to-many
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }

    // Many-to-many via join
    public List<AssignmentTag> AssignmentTags { get; set; } = new();
}
