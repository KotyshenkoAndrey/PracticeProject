namespace Practice.Context;

using Microsoft.EntityFrameworkCore;

public class DbContextFactory
{
    private readonly DbContextOptions<AppDbContext> options;

    public DbContextFactory(DbContextOptions<AppDbContext> options)
    {
        this.options = options;
    }

    public AppDbContext Create()
    {
        return new AppDbContext(options);
    }
}
