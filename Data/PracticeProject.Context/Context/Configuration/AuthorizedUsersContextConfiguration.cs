namespace PracticeProject.Context;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities.Identity;

public static class AuthorizedUsersContextConfiguration
{
    public static void ConfigureAuthorizedUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorizedUsers>().ToTable("authorizedUsers");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("authorizedUsers_roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("authorizedUsers_tokens");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("authorizedUsers_role_owners");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("authorizedUsers_role_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("authorizedUsers_logins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("authorizedUsers_claims");
    }
}