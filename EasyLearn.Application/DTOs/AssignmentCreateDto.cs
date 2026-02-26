namespace EasyLearn.Application.DTOs;

public class AssignmentCreateDto
{
    public int SubjectId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
}