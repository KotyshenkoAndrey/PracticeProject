using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Context
{
    public static class CarContextConfiguration
    {
            public static void ConfigureCar(this ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Car>().ToTable("car");
                modelBuilder.Entity<Car>()
                            .Property(e => e.DatePosted)
                            .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                modelBuilder.Entity<Car>().HasOne(x => x.Seller).WithMany(x => x.CarsUser).HasForeignKey(x => x.SellerId).OnDelete(DeleteBehavior.Restrict);
            }
    }
}
