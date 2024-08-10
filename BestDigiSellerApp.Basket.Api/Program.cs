using BestDigiSeller.JWT;
using BestDigiSellerApp.Basket.Api.Extensions;
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
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.SwaggerGenSettings();
    builder.Services.CreateProductClient();
    builder.Services.CreateStripeClient();
    builder.Services.ServiceLifetimeOptions();
    builder.Services.DbContextConfiguration(builder.Configuration);
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Basket.Application")));
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddProblemDetails();
    builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
    })
.AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
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
