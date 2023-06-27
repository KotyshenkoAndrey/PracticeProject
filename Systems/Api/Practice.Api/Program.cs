using Practice.Api.Configuration;
using Practice.Api;
using Practice.Settings;
using Practice.Services.Settings;
using Practice.Context;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<IdentitySettings>("Main");
var identitySettings = Settings.Load<IdentitySettings>("Identity");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
builder.AddAppLogger();

// Add services to the container.
var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppDbContext();
services.AddAppVersioning();

services.AddAppHealthChecks();
services.AddAppSwagger(identitySettings, swaggerSettings);
services.AddAppAutoMappers();

services.AddAppAuth(identitySettings);

services.AddAppControllerAndViews();
services.RegisterAppServices();

var app = builder.Build();
app.UseAppHealthChecks();
app.UseAppSwagger();
DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);
app.UseAppMiddlewares();
app.UseAppAuth();
app.UseAppControllerAndViews();

app.Run();
