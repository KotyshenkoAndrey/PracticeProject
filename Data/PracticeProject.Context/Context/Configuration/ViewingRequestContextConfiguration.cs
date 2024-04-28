using Microsoft.EntityFrameworkCore;

namespace PracticeProject.Context.Entities
{

    public static class ViewingRequestContextConfiguration
    {
        public static void ConfigureViewingRequest(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewingRequest>().ToTable("viewingrequest");
            modelBuilder.Entity<ViewingRequest>()
                        .Property(e => e.RequestDate)
                        .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            modelBuilder.Entity<ViewingRequest>().HasOne(x => x.Car).WithMany(s => s.ViewingRequestsCar).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ViewingRequest>().HasOne(x => x.Sender).WithMany(s => s.ViewingRequestsUser).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
