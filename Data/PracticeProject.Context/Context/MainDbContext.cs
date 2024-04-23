namespace PracticeProject.Context;

using PracticeProject.Context.Entities;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class MainDbContext : IdentityDbContext<AuthorizedUsers, IdentityRole<Guid>, Guid>
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<ViewingRequest> ViewingRequests { get; set; }
    public DbSet<AuthorizedUsers> AuthorizedUsers { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureCar();
        modelBuilder.ConfigureSeller();
        modelBuilder.ConfigureViewingRequest();
        modelBuilder.ConfigureAuthorizedUsers();
    }
}
