using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Context.Entities
{
    public static class UserContextConfiguration
    {
        public static void ConfigureUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");           
        }
    }
}
