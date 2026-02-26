using EasyLearn.Domain.Entities;

namespace EasyLearn.Application.Interfaces;

public interface IAssignmentRepository
{
    Task<List<Assignment>> GetAllAsync();
    Task<Assignment?> GetByIdAsync(int id);
    Task<Assignment> CreateAsync(Assignment assignment);
    Task<bool> UpdateAsync(Assignment assignment);
    Task<bool> DeleteAsync(int id);
    Task<bool> SubjectExistsAsync(int subjectId);
}