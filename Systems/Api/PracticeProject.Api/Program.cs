using PracticeProject.Api;
using PracticeProject.Api.Configuration;
using PracticeProject.Services.Logger;
using PracticeProject.Services.Settings;
using PracticeProject.Settings;
using PracticeProject.Context;
using PracticeProject.Context.Seeder;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSetting = Settings.Load<SwaggerSettings>("Swagger");


var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;
// Add services to the container.

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppAutoMappers();
services.AddAppValidator();
services.AddAppHealthChecks();
services.AddAppVersioning();

services.AddAppCors();
services.AddAppControllerAndViews();
services.AddAppSwagger(mainSettings, swaggerSetting);

services.RegisterServices();


var app = builder.Build();
var logger = app.Services.GetRequiredService<IAppLogger>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseAppHealthChecks();
app.UseAppCors();
app.UseAppControllerAndViews();
app.UseAppSwagger();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services);

app.Run();
