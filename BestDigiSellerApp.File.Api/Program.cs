using BestDigiSellerApp.File.Api.Extensions;
using Serilog;
using BestDigiSeller.JWT;
using Microsoft.Extensions.FileProviders;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()/*WriteTo.File(DateTime.UtcNow.ToString("dd/mm/yy"))*/
        .CreateLogger();
try
{

    Log.Information("Web host is being started  . . .");
    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();

    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.SwaggerGenSettings();
    builder.Services.VersioningSettings();
    var app = builder.Build();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Media")),
        RequestPath = "/Files"
    });
    app.MapDefaultEndpoints();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

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
