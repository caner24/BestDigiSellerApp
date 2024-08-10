using BestDigiSeller.JWT;
using BestDigiSellerApp.Discount.Api.Extensions;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();
    builder.Services.DbContextConfiguration(builder.Configuration);
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddMassTransit(builder.Configuration);
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Discount.Application")));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.VersioningSettings();
    builder.Services.ServiceLifetimeOptions(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.SwaggerGenSettings();
    builder.Services.CreateUserClient();
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