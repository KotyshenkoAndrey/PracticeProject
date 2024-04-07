namespace PracticeProject.Api;
using PracticeProject.Services.Settings;
using PracticeProject.Services.Logger;
using PracticeProject.Api.Settings;
using PracticeProject.Services.Cars;
using PracticeProject.Context.Seeder;

public static class Bootstrapper
{

    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddAppLogger()
            .AddDbSeeder()
            .AddApiSpecialSettings()
            .AddCarService()
            ;

        return services;
    }
}
