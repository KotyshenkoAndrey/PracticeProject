namespace PracticeProject.Test.Api;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PracticeProject.Context;
using PracticeProject.Services.Actions;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Sellers;
using PracticeProject.Services.RabbitMq;
using PracticeProject.Services.Settings;
using PracticeProject.Services.AuthorizedUsers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using PracticeProject.Services.Cars.Models;
using PracticeProject.Services.AuthorizedUsersAccount;

public class ApiAppFactory : WebApplicationFactory<PracticeProject.Api.Program>
{
    private readonly string _dbConnStr;
    private readonly Func<HttpClient> _getBackchannelClient;

    public ApiAppFactory(string host, int port, string password, Func<HttpClient> getBackchannelClient)
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

        _getBackchannelClient = getBackchannelClient;
    }

    private void RemoveService(IServiceCollection services, Type serviceType)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == serviceType);
        if (descriptor != null)
            services.Remove(descriptor);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //use for load and init service
        var car = new CarViewModel();
        var seller = new SellerViewModel();
        var user = new AuthorizedUsersAccountModel();

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
            RemoveService(services, typeof(SwaggerSettings));
            RemoveService(services, typeof(IAction));
            RemoveService(services, typeof(IRabbitMq));
            
            // Register new services for tests
            services.AddSingleton(testConfiguration);
            services.AddAppDbContext(testConfiguration);
            services.AddMainSettings(testConfiguration);
            services.AddSwaggerSettings(testConfiguration);

            services.AddSingleton(new Mock<IAction>().Object);
            services.AddSingleton(new Mock<IRabbitMq>().Object);

            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    var client = _getBackchannelClient.Invoke();
                    options.Backchannel = client;
                });
        });
    }
}