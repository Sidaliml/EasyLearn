using EasyLearn.Application.Interfaces;
using EasyLearn.Domain.Entities;

namespace EasyLearn.Application.Services;

public class TagService
{
    private readonly ITagRepository _repo;

    public TagService(ITagRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Tag>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Tag?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task<Tag?> CreateAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        name = name.Trim();
        var existing = await _repo.GetByNameAsync(name);
        if (existing != null) return null;

        return await _repo.CreateAsync(new Tag { Name = name });
    }

    public async Task<Tag?> UpdateAsync(int id, string name)
    {
        if (id <= 0) return null;
        if (string.IsNullOrWhiteSpace(name)) return null;

        name = name.Trim();
        return await _repo.UpdateAsync(id, name);
    }

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    public Task<List<Tag>> GetTagsForAssignmentAsync(int assignmentId)
        => _repo.GetTagsForAssignmentAsync(assignmentId);

    public async Task<bool> AddTagToAssignmentAsync(int assignmentId, int tagId)
    {
        if (!await _repo.AssignmentExistsAsync(assignmentId)) return false;
        if (!await _repo.TagExistsAsync(tagId)) return false;

        return await _repo.AddTagToAssignmentAsync(assignmentId, tagId);
    }

    public Task<bool> RemoveTagFromAssignmentAsync(int assignmentId, int tagId)
        => _repo.RemoveTagFromAssignmentAsync(assignmentId, tagId);
}