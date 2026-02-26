namespace EasyLearn.Application.DTOs;

public class AssignmentReadDto
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }
}