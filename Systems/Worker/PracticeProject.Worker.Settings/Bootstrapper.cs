namespace PracticeProject.Worker.Settings;

using PracticeProject.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

    public static class Bootstrapper
    {
        public static IServiceCollection AddMailSettings(this IServiceCollection services, IConfiguration configuration = null)
        {
            var settings = Settings.Load<MailSettings>("MailSettings", configuration);
            services.AddSingleton(settings);

            return services;
        }
    }
