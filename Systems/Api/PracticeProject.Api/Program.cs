using PracticeProject.Api;
using PracticeProject.Api.Configuration;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Settings;
using PracticeProject.Settings;
using PracticeProject.Context;
using PracticeProject.Context.Seeder;
using PracticeProject.Services.AppHubs;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSetting = Settings.Load<SwaggerSettings>("Swagger");
var identitySetting = Settings.Load<IdentitySettings>("Identity");


var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;
// Add services to the container.
services.AddAppCors();
services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);


services.AddSignalR();

services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSetting, identitySetting);

services.AddAppAutoMappers();

services.AddAppValidator();

services.AddAppAuth(identitySetting);

services.AddAppControllerAndViews();

services.RegisterServices(builder.Configuration);


var app = builder.Build();
app.UseAppCors();
var logger = app.Services.GetRequiredService<IAppLogger>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseAppHealthChecks();

app.UseAppControllerAndViews();
app.UseAppSwagger();
app.UseAppAuth();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services);

app.MapHub<AppHub>("/appHub");

app.Run();
