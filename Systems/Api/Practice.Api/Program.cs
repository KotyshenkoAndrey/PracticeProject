using Practice.Api.Configuration;
using Practice.Api;
using Practice.Settings;
using Practice.Services.Settings;
using Practice.Context;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
builder.AddAppLogger();

// Add services to the container.
var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppDbContext();
services.AddAppVersioning();

services.AddAppHealthChecks();
services.AddAppSwagger(mainSettings, swaggerSettings);
services.AddAppAutoMappers();
services.AddAppControllerAndViews();
services.RegisterAppServices();

var app = builder.Build();
app.UseAppHealthChecks();
app.UseAppSwagger();
DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

app.UseAppControllerAndViews();
app.UseAppMiddlewares();
app.Run();
