namespace PracticeProject.Services.AuthorizedUsers;

using Microsoft.Extensions.DependencyInjection;
using PracticeProject.Services.AuthorizedUsersAccount;

public static class Bootstrapper
{
    public static IServiceCollection AddAuthorizedUsersAccountService(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizedUsersAccountService, AuthorizedUsersAccountService>();

        return services;
    }
}
