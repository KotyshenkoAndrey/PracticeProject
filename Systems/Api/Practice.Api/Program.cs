using Practice.Api.Configuration;
using Practice.Api;
using Practice.Settings;
using Practice.Services.Settings;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
builder.AddAppLogger();

// Add services to the container.
var services = builder.Services;
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppVersioning();
services.RegisterAppServices();

services.AddAppHealthChecks();
services.AddAppSwagger(mainSettings, swaggerSettings);
services.AddAppControllerAndViews();


builder.Services.AddControllers();

var app = builder.Build();

app.UseAppHealthChecks();
app.UseAppSwagger();

app.UseAppControllerAndViews();

app.Run();
