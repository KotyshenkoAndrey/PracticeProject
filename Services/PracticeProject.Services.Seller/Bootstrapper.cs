namespace PracticeProject.Services.Sellers;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddSellerService(this IServiceCollection services)
    {
        return services.AddSingleton<ISellerService, SellerService>();
    }
}