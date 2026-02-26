using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EasyLearn.Infrastructure.Data;

public class EasyLearnDbContextFactory : IDesignTimeDbContextFactory<EasyLearnDbContext>
{
    public EasyLearnDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EasyLearnDbContext>();

        // Samma server som i appsettings.json
        var connectionString =
            "Server=Sid-Ali\\SQLEXPRESS01;Database=EasyLearnDb;Trusted_Connection=True;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);

        return new EasyLearnDbContext(optionsBuilder.Options);
    }
}
