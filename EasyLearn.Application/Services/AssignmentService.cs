using EasyLearn.Application.DTOs;
using EasyLearn.Application.Interfaces;
using EasyLearn.Domain.Entities;

namespace EasyLearn.Application.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IAssignmentRepository _repo;

    public AssignmentService(IAssignmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AssignmentReadDto>> GetAllAsync()
    {
        var assignments = await _repo.GetAllAsync();

        return assignments.Select(a => new AssignmentReadDto
        {
            Id = a.Id,
            SubjectId = a.SubjectId,
            SubjectName = a.Subject?.Name ?? "",
            Title = a.Title,
            Description = a.Description,
            Deadline = a.Deadline,
            IsCompleted = a.IsCompleted
        }).ToList();
    }

    public async Task<AssignmentReadDto?> GetByIdAsync(int id)
    {
        var a = await _repo.GetByIdAsync(id);
        if (a == null) return null;

        return new AssignmentReadDto
        {
            Id = a.Id,
            SubjectId = a.SubjectId,
            SubjectName = a.Subject?.Name ?? "",
            Title = a.Title,
            Description = a.Description,
            Deadline = a.Deadline,
            IsCompleted = a.IsCompleted
        };
    }

    public async Task<AssignmentReadDto?> CreateAsync(AssignmentCreateDto dto)
    {
        var subjectExists = await _repo.SubjectExistsAsync(dto.SubjectId);
        if (!subjectExists) return null;

        var assignment = new Assignment
        {
            SubjectId = dto.SubjectId,
            Title = dto.Title,
            Description = dto.Description,
            Deadline = dto.Deadline,
            IsCompleted = false
        };

        var created = await _repo.CreateAsync(assignment);

        // Hämta igen för att få SubjectName via Include
        var full = await _repo.GetByIdAsync(created.Id);
        if (full == null) return null;

        return new AssignmentReadDto
        {
            Id = full.Id,
            SubjectId = full.SubjectId,
            SubjectName = full.Subject?.Name ?? "",
            Title = full.Title,
            Description = full.Description,
            Deadline = full.Deadline,
            IsCompleted = full.IsCompleted
        };
    }

    public async Task<bool> UpdateAsync(int id, AssignmentUpdateDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        var subjectExists = await _repo.SubjectExistsAsync(dto.SubjectId);
        if (!subjectExists) return false;

        existing.SubjectId = dto.SubjectId;
        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Deadline = dto.Deadline;
        existing.IsCompleted = dto.IsCompleted;

        return await _repo.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);

    public async Task<bool> ToggleCompletedAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        existing.IsCompleted = !existing.IsCompleted;
        return await _repo.UpdateAsync(existing);
    }
}