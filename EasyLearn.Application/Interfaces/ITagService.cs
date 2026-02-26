using EasyLearn.Application.DTOs;

namespace EasyLearn.Application.Interfaces;

public interface ITagService
{
    Task<List<TagReadDto>> GetAllAsync();
    Task<TagReadDto?> CreateAsync(TagCreateDto dto);
    Task<bool> DeleteAsync(int id);

    Task<List<TagReadDto>?> GetTagsForAssignmentAsync(int assignmentId);
    Task<bool> AddTagToAssignmentAsync(int assignmentId, int tagId);
    Task<bool> RemoveTagFromAssignmentAsync(int assignmentId, int tagId);
}