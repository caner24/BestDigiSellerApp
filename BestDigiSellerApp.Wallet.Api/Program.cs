using BestDigiSellerApp.Wallet.Api.Extensions;
using BestDigiSeller.JWT;
using System.Reflection;
using Serilog;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()/*WriteTo.File(DateTime.UtcNow.ToString("dd/mm/yy"))*/
        .CreateLogger();
try
{

    Log.Information("Web host is being started  . . .");
    var builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults();
builder.Services.DbContextConfiguration(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Wallet.Application")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.ServiceLifetimeOptions(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddJwtAuthentication(builder.Configuration);

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
