using BestDigiSellerApp.Product.Api.Extensions;
using BestDigiSeller.JWT;
using Serilog;
using Aspire.StackExchange.Redis;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using BestDigiSellerApp.Product.Application.Validation.FluentValidation;
Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()/*WriteTo.File(DateTime.UtcNow.ToString("dd/mm/yy"))*/
        .CreateLogger();
try
{

    Log.Information("Web host is being started  . . .");
    var builder = WebApplication.CreateBuilder(args);
    builder.AddServiceDefaults();
    builder.Services.ServiceLifetimeOptions();
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>();
    builder.Services.CreateFileClient();
    builder.AddRedisDistributedCache("redis");
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Product.Application")));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.VersioningSettings();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.SwaggerGenSettings();

    builder.Services.DbContextConfiguration(builder.Configuration);


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
