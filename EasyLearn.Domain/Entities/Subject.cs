namespace EasyLearn.Domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Assignment> Assignments { get; set; } = new();
}
