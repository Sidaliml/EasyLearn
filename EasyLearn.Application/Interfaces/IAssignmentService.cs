using EasyLearn.Application.DTOs;

namespace EasyLearn.Application.Interfaces;

public interface IAssignmentService
{
    Task<List<AssignmentReadDto>> GetAllAsync();
    Task<AssignmentReadDto?> GetByIdAsync(int id);
    Task<AssignmentReadDto?> CreateAsync(AssignmentCreateDto dto);
    Task<bool> UpdateAsync(int id, AssignmentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ToggleCompletedAsync(int id);
}