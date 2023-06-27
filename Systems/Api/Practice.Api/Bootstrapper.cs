namespace Practice.Api;

using Practice.Api.Settings;
using Practice.Services.Settings;
using Practice.Services.UserAccount;
using Practice.Services.Books;
using Microsoft.Extensions.DependencyInjection;

    public static class Bootstrapper
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services
                .AddMainSettings()
                .AddSwaggerSettings()
                .AddIdentitySettings()
                .AddApiSpecialSettings()
                .AddBookService()
                .AddUserAccountService()
                ;

            return services;
        }
    }

