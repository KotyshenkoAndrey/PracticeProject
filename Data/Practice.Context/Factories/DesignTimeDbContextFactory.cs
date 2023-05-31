namespace Practice.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private const string migrationProjctPrefix = "Practice.Context.Migrations";

    public AppDbContext CreateDbContext(string[] args)
    {
        var provider = (args?[0] ?? $"{DbType.MSSQL}").ToLower();

        var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.context.json")
             .Build();


        DbContextOptions<AppDbContext> options;
        if (provider.Equals($"{DbType.MSSQL}".ToLower()))
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(
                        configuration.GetConnectionString(provider),
                        opts => opts
                            .MigrationsAssembly($"{migrationProjctPrefix}{DbType.MSSQL}")
                    )
                    .Options;
        }
        else
        if (provider.Equals($"{DbType.PostgreSQL}".ToLower()))
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(
                        configuration.GetConnectionString(provider),
                        opts => opts
                            .MigrationsAssembly($"{migrationProjctPrefix}{DbType.PostgreSQL}")
                    )
                    .Options;
        }
        else
        {
            throw new Exception($"Unsupported provider: {provider}");
        }

        var dbf = new DbContextFactory(options);
        return dbf.Create();
    }
}
