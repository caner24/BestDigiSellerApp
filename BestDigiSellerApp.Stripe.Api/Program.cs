using BestDigiSeller.JWT;
using BestDigiSellerApp.Stripe.Api.Extensions;
using Stripe;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
StripeConfiguration.ApiKey = builder.Configuration["StripeApiKey"];
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.CustomCorsPolicy();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BestDigiSellerApp.Stripe.Application")));
builder.Services.AddControllers();
builder.Services.ServiceLifetimeOptions();
builder.Services.CustomCorsPolicy();
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
