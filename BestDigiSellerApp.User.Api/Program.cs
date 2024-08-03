using BestDigiSeller.JWT;
using BestDigiSellerApp.User.Api.Extensions;
using BestDigiSellerApp.User.Data.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.IdentityConfiguration(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.CreateWalletClient();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.User.Application")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ServiceLifetimeOptions(builder.Configuration);
builder.Services.AddMassTransit(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSwaggerGen();

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
