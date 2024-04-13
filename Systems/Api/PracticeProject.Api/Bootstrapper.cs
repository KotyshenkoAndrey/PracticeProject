namespace PracticeProject.Api;
using PracticeProject.Services.Settings;
using PracticeProject.Services.Logger;
using PracticeProject.Api.Settings;
using PracticeProject.Services.Cars;
using PracticeProject.Context.Seeder;
using PracticeProject.Services.RabbitMq;
using PracticeProject.Services.Actions;

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
            .AddRabbitMq()
            .AddActions()
            ;

        return services;
    }
}
