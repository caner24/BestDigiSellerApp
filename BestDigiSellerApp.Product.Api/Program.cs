using BestDigiSellerApp.Product.Api.Extensions;
using BestDigiSeller.JWT;
using Serilog;
using Aspire.StackExchange.Redis;
Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()/*WriteTo.File(DateTime.UtcNow.ToString("dd/mm/yy"))*/
        .CreateLogger();
try
{

    Log.Information("Web host is being started  . . .");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.AddServiceDefaults();
    builder.AddRedisDistributedCache("redis");
    builder.Services.VersioningSettings();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.DbContextConfiguration(builder.Configuration);
    builder.Services.AddJwtAuthentication(builder.Configuration);

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
