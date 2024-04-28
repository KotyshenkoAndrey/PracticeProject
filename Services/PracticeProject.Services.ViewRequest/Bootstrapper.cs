namespace PracticeProject.Services.ViewingRequests;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddViewRequstService(this IServiceCollection services)
    {
        return services.AddSingleton<IViewRequest, ViewRequestService>();
    }
}