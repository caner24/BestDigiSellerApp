using Serilog;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();


    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

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
