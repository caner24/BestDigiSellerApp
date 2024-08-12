using BestDigiSeller.JWT;
using BestDigiSellerApp.Stripe.Api.Extensions;
using Serilog;
using Stripe;
using System.Reflection;


Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();
    StripeConfiguration.ApiKey = builder.Configuration["StripeApiKey"];
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.CustomCorsPolicy();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Stripe.Application")));
    builder.Services.AddControllers();
    builder.Services.AddMassTransit(builder.Configuration);
    builder.Services.DbContextConfiguration(builder.Configuration);
    builder.Services.CreateWalletClient();
    builder.Services.CreateProductClient();
    builder.Services.ServiceLifetimeOptions(builder.Configuration);
    builder.Services.CustomCorsPolicy();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Stripe.Application")));
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.SwaggerGenSettings();

    var app = builder.Build();
    app.MapDefaultEndpoints();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler();
    app.UseCors("CustomCorsPolicy");
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
