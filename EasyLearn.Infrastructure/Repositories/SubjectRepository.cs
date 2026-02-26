using EasyLearn.Application.Interfaces;
using EasyLearn.Domain.Entities;
using EasyLearn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Infrastructure.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly EasyLearnDbContext _db;

    public SubjectRepository(EasyLearnDbContext db)
    {
        _db = db;
    }

    public async Task<List<Subject>> GetAllAsync()
        => await _db.Subjects.ToListAsync();

    public async Task<Subject?> GetByIdAsync(int id)
        => await _db.Subjects.FindAsync(id);

    public async Task<Subject> CreateAsync(string name)
    {
        var subject = new Subject { Name = name };
        _db.Subjects.Add(subject);
        await _db.SaveChangesAsync();
        return subject;
    }

    public async Task<bool> UpdateAsync(int id, string name)
    {
        var subject = await _db.Subjects.FindAsync(id);
        if (subject == null) return false;

        subject.Name = name;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var subject = await _db.Subjects.FindAsync(id);
        if (subject == null) return false;

        _db.Subjects.Remove(subject);
        await _db.SaveChangesAsync();
        return true;
    }
}