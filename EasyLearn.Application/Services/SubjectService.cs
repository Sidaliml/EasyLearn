using EasyLearn.Application.DTOs;
using EasyLearn.Application.Interfaces;

namespace EasyLearn.Application.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _repo;

    public SubjectService(ISubjectRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SubjectReadDto>> GetAllAsync()
    {
        var subjects = await _repo.GetAllAsync();
        return subjects.Select(s => new SubjectReadDto { Id = s.Id, Name = s.Name }).ToList();
    }

    public async Task<SubjectReadDto?> GetByIdAsync(int id)
    {
        var subject = await _repo.GetByIdAsync(id);
        if (subject == null) return null;

        return new SubjectReadDto { Id = subject.Id, Name = subject.Name };
    }

    public async Task<SubjectReadDto> CreateAsync(SubjectCreateDto dto)
    {
        var created = await _repo.CreateAsync(dto.Name);
        return new SubjectReadDto { Id = created.Id, Name = created.Name };
    }

    public async Task<bool> UpdateAsync(int id, SubjectUpdateDto dto)
        => await _repo.UpdateAsync(id, dto.Name);

    public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);
}
