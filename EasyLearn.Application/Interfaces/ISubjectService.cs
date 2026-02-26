using EasyLearn.Application.DTOs;

namespace EasyLearn.Application.Interfaces;

public interface ISubjectService
{
    Task<List<SubjectReadDto>> GetAllAsync();
    Task<SubjectReadDto?> GetByIdAsync(int id);
    Task<SubjectReadDto> CreateAsync(SubjectCreateDto dto);
    Task<bool> UpdateAsync(int id, SubjectUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
