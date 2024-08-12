﻿using Asp.Versioning;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Stripe.Api.Consumer;
using BestDigiSellerApp.Stripe.Application.Clients;
using BestDigiSellerApp.Stripe.Data.Abstract;
using BestDigiSellerApp.Stripe.Data.Concrete;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace BestDigiSellerApp.Stripe.Api.Extensions
{
    public static class ServiceExtension
    {

        public static void CustomCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                 .WithExposedHeaders(new[] { "Location" })

                );
            });
        }
        public static void CreateWalletClient(this IServiceCollection services)
        {
            services.AddHttpClient<ProductClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-wallet-api");
            });
        }
        public static void CreateProductClient(this IServiceCollection services)
        {
            services.AddHttpClient<WalletClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-wallet-api");
            });
        }

        public static void AddMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<DecreaseStockConsume>();
                x.AddConsumer<InvoiceMailSenderConsume>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config.GetConnectionString("messaging");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("decrease-stock", e =>
                    {
                        e.ConfigureConsumer<DecreaseStockConsume>(context);
                    });
                    cfg.ReceiveEndpoint("invoice-mail-sender", e =>
                    {
                        e.ConfigureConsumer<InvoiceMailSenderConsume>(context);
                    });
                });
            });
        }

        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("InvoiceSqlConnection");
            services.AddDbContext<StripeContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.Stripe.Api");
            }
           ));
        }
        public static void ServiceLifetimeOptions(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });
        }

        public static void SwaggerGenSettings(this IServiceCollection services)
        {
            services.AddSwaggerGen(_ =>
            {
                _.SwaggerDoc("v1", new OpenApiInfo { Title = "BestDigiSellerApp", Version = "v1" });
                _.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                _.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
        }
        public static void VersioningSettings(this IServiceCollection services)
        {
            var apiVersioningBuilder = services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });
            apiVersioningBuilder.AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
        }

        public static void ServiceLifetimeOptions(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IInvoiceDal, InvoiceDal>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });
            var emailConfig = config
       .GetSection("EmailConfiguration")
       .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);


        }
    }
}
