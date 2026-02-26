using EasyLearn.Application.Interfaces;
using EasyLearn.Domain.Entities;
using EasyLearn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly EasyLearnDbContext _db;

    public TagRepository(EasyLearnDbContext db)
    {
        _db = db;
    }

    public async Task<List<Tag>> GetAllAsync()
        => await _db.Tags.ToListAsync();

    public async Task<Tag?> GetByIdAsync(int id)
        => await _db.Tags.FindAsync(id);

    public async Task<Tag?> GetByNameAsync(string name)
        => await _db.Tags.FirstOrDefaultAsync(t => t.Name == name);

    public async Task<Tag> CreateAsync(Tag tag)
    {
        _db.Tags.Add(tag);
        await _db.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> UpdateAsync(int id, string name)
    {
        var tag = await _db.Tags.FindAsync(id);
        if (tag == null) return null;

        tag.Name = name;
        await _db.SaveChangesAsync();
        return tag;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await _db.Tags.FindAsync(id);
        if (tag == null) return false;

        _db.Tags.Remove(tag);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignmentExistsAsync(int assignmentId)
        => await _db.Assignments.AnyAsync(a => a.Id == assignmentId);

    public async Task<bool> TagExistsAsync(int tagId)
        => await _db.Tags.AnyAsync(t => t.Id == tagId);

    public async Task<bool> AddTagToAssignmentAsync(int assignmentId, int tagId)
    {
        var exists = await _db.AssignmentTags
            .AnyAsync(x => x.AssignmentId == assignmentId && x.TagId == tagId);

        if (exists) return false;

        _db.AssignmentTags.Add(new AssignmentTag
        {
            AssignmentId = assignmentId,
            TagId = tagId
        });

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveTagFromAssignmentAsync(int assignmentId, int tagId)
    {
        var link = await _db.AssignmentTags
            .FirstOrDefaultAsync(x => x.AssignmentId == assignmentId && x.TagId == tagId);

        if (link == null) return false;

        _db.AssignmentTags.Remove(link);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Tag>> GetTagsForAssignmentAsync(int assignmentId)
    {
        return await _db.AssignmentTags
            .Where(x => x.AssignmentId == assignmentId)
            .Include(x => x.Tag)
            .Select(x => x.Tag!)
            .ToListAsync();
    }
}