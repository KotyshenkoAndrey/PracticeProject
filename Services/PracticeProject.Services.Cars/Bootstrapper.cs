namespace PracticeProject.Services.Cars;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddCarService(this IServiceCollection services)
    {
        return services.AddSingleton<ICarService, CarService>();
    }
}