namespace PracticeProject.Api;
using PracticeProject.Services.Settings;
using PracticeProject.Services.Logger;
public static class Bootstrapper
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
        services.AddMainSettings()
        .AddSwaggerSettings()
        .AddLogSettings()
        .AddAppLogger();
            return services;
        }
    }
