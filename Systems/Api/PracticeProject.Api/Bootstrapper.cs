﻿namespace PracticeProject.Api;
using PracticeProject.Services.Settings;
using PracticeProject.Services.Logger;
using PracticeProject.Api.Settings;
using PracticeProject.Services.Cars;
using PracticeProject.Services.Sellers;
using PracticeProject.Context.Seeder;
using PracticeProject.Services.RabbitMq;
using PracticeProject.Services.Actions;
using PracticeProject.Services.AuthorizedUsers;
using PracticeProject.Services.ViewingRequests;
using PracticeProject.Services.Cache;

public static class Bootstrapper
{

    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddDbSeeder()
            .AddApiSpecialSettings()
            .AddCarService()
            .AddSellerService()
            .AddViewRequstService()
            .AddRabbitMq()
            .AddActions()
            .AddAuthorizedUsersAccountService()
            .AddCache()
            ;

        return services;
    }
}
