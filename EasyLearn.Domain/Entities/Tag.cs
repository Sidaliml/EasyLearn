namespace EasyLearn.Domain.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<AssignmentTag> AssignmentTags { get; set; } = new();
}
