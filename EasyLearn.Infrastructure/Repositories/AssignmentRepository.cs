using EasyLearn.Application.Interfaces;
using EasyLearn.Domain.Entities;
using EasyLearn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Infrastructure.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly EasyLearnDbContext _db;

    public AssignmentRepository(EasyLearnDbContext db)
    {
        _db = db;
    }

    public async Task<List<Assignment>> GetAllAsync()
        => await _db.Assignments.Include(a => a.Subject).ToListAsync();

    public async Task<Assignment?> GetByIdAsync(int id)
        => await _db.Assignments.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Assignment> CreateAsync(Assignment assignment)
    {
        _db.Assignments.Add(assignment);
        await _db.SaveChangesAsync();
        return assignment;
    }

    public async Task<bool> UpdateAsync(Assignment assignment)
    {
        _db.Assignments.Update(assignment);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var assignment = await _db.Assignments.FindAsync(id);
        if (assignment == null) return false;

        _db.Assignments.Remove(assignment);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SubjectExistsAsync(int subjectId)
        => await _db.Subjects.AnyAsync(s => s.Id == subjectId);
}