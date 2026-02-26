using EasyLearn.Domain.Entities;
using EasyLearn.Infrastructure.Data;
using EasyLearn.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EasyLearn.Tests;

public class SubjectRepositoryTests
{
    private static EasyLearnDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<EasyLearnDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EasyLearnDbContext(options);
    }

    [Fact]
    public async Task Create_And_GetById_Works()
    {
        using var db = CreateDbContext();
        var repo = new SubjectRepository(db);

      
        var created = await repo.CreateAsync("Matte");

        var fromDb = await repo.GetByIdAsync(created.Id);

        Assert.NotNull(fromDb);
        Assert.Equal("Matte", fromDb!.Name);
    }

    [Fact]
    public async Task GetAll_Returns_Items()
    {
        using var db = CreateDbContext();
        var repo = new SubjectRepository(db);

        await repo.CreateAsync("Matte");
        await repo.CreateAsync("Svenska");

        var all = await repo.GetAllAsync();

        Assert.Equal(2, all.Count);
        Assert.Contains(all, s => s.Name == "Matte");
        Assert.Contains(all, s => s.Name == "Svenska");
    }

    [Fact]
    
    public async Task Update_Works()
    {
        using var db = CreateDbContext();
        var repo = new SubjectRepository(db);

        var created = await repo.CreateAsync("Matte");

        // Din UpdateAsync returnerar bool (true/false)
        var ok = await repo.UpdateAsync(created.Id, "Matte (UPPDATERAD)");
        Assert.True(ok);

        // Hämta igen och verifiera
        var fromDb = await repo.GetByIdAsync(created.Id);
        Assert.NotNull(fromDb);
        Assert.Equal("Matte (UPPDATERAD)", fromDb!.Name);
    }

    [Fact]
    public async Task Delete_Works()
    {
        using var db = CreateDbContext();
        var repo = new SubjectRepository(db);

        var created = await repo.CreateAsync("Matte");

        var deleted = await repo.DeleteAsync(created.Id);
        var afterDelete = await repo.GetByIdAsync(created.Id);

        Assert.True(deleted);
        Assert.Null(afterDelete);
    }
}