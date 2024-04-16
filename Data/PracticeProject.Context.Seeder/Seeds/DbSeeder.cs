namespace PracticeProject.Context.Seeder;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PracticeProject.Context.Entities.Identity;
using PracticeProject.Services.AuthorizedUsersAccount;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static MainDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
            {
                await AddDemoData(serviceProvider);
                await AddAdministrator(serviceProvider);
            })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        if (await context.Cars.AnyAsync())
            return;

        await context.Cars.AddRangeAsync(new DemoHelper().GetCars);

        await context.SaveChangesAsync();
    }
    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        var userAccountService = scope.ServiceProvider.GetService<IAuthorizedUsersAccountService>();
        if (!(await userAccountService.IsEmpty())) return;
        await userAccountService.Create(new RegisterAuthorizedUsersAccountModel()
        {
            Name = settings.Init.Administrator.Name,
            Email = settings.Init.Administrator.Email,
            Password = settings.Init.Administrator.Password,
        });

        await using var context = DbContext(serviceProvider);

        if (await context.Cars.AnyAsync())
            return;

        await context.Cars.AddRangeAsync(new DemoHelper().GetCars);

        await context.SaveChangesAsync();
    }
}