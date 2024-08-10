using BestDigiSeller.JWT;
using BestDigiSellerApp.User.Api.Extensions;
using BestDigiSellerApp.User.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
try
{

    Log.Information("Web host is being started  . . .");
    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();
    builder.Services.IdentityConfiguration(builder.Configuration);
    builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson(opt =>
        opt.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddProblemDetails();
    builder.Services.CreateWalletClient();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.User.Application")));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.ServiceLifetimeOptions(builder.Configuration);
    builder.Services.AddMassTransit(builder.Configuration);
    builder.Services.VersioningSettings();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    builder.Services.SwaggerGenSettings();

    var app = builder.Build();

    app.MapDefaultEndpoints();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An exception happened while project was started.");

}
finally
{
    Log.CloseAndFlush();

}
