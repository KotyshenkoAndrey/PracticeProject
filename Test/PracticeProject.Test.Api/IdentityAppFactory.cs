namespace PracticeProject.Test.Api;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeProject.Context;
using PracticeProject.Services.Settings;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

public class IdentityAppFactory : WebApplicationFactory<PracticeProject.Identity.Program>
{
    private readonly string _dbConnStr;

    public IdentityAppFactory(string host, int port, string password)
    {
        var sb = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = port,
            Database = "test_database",
            Username = "postgres",
            Password = password
        };
        _dbConnStr = sb.ConnectionString;
    }

    private void RemoveService(IServiceCollection services, Type serviceType)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == serviceType);
        if (descriptor != null)
            services.Remove(descriptor);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var overriddenVariables = new List<KeyValuePair<string, string>>()
            {
                new("Database:Type", "PgSql"),
                new("Database:ConnectionString", _dbConnStr)
            };

            var testConfiguration = ConfigurationFactory.Create(overriddenVariables);

            // Delete old API work settings
            RemoveService(services, typeof(IConfiguration));
            RemoveService(services, typeof(DbContextOptions<MainDbContext>));
            RemoveService(services, typeof(DbSettings));
            RemoveService(services, typeof(MainSettings));

            // Register new services for tests
            services.AddSingleton(testConfiguration);
            services.AddAppDbContext(testConfiguration);
            services.AddMainSettings(testConfiguration);
        });
    }
}