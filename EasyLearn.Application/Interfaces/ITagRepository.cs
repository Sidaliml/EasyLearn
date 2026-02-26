using EasyLearn.Domain.Entities;

namespace EasyLearn.Application.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync();
    Task<Tag?> GetByIdAsync(int id);
    Task<Tag?> GetByNameAsync(string name);

    Task<Tag> CreateAsync(Tag tag);
    Task<Tag?> UpdateAsync(int id, string name);
    Task<bool> DeleteAsync(int id);

    Task<bool> AssignmentExistsAsync(int assignmentId);
    Task<bool> TagExistsAsync(int tagId);
    Task<bool> AddTagToAssignmentAsync(int assignmentId, int tagId);
    Task<bool> RemoveTagFromAssignmentAsync(int assignmentId, int tagId);
    Task<List<Tag>> GetTagsForAssignmentAsync(int assignmentId);
}