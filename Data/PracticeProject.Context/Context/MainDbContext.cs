namespace PracticeProject.Context;

using PracticeProject.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class MainDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ViewingRequest> ViewingRequests { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureCar();
        modelBuilder.ConfigureUser();
        modelBuilder.ConfigureViewingRequest();                  
    }
}
