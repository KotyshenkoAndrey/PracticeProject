namespace PracticeProject.Api.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PracticeProject.Services.Settings;
using System.Linq;

/// <summary>
/// CORS configuration
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Add CORS
    /// </summary>
    /// <param name="services">Services collection</param>
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddDefaultPolicy(pol =>
            {
                pol.AllowAnyHeader();
                pol.AllowAnyMethod();
                pol.AllowAnyOrigin();
            });
        });
        return services;
    }

    /// <summary>
    /// Use service
    /// </summary>
    /// <param name="app">Application</param>
    public static void UseAppCors(this WebApplication app)
    {
        app.UseCors();
    }
}