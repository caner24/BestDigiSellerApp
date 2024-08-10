
using Asp.Versioning;
using BestDigiSellerApp.Basket.Api.ActionFilters;
using BestDigiSellerApp.Basket.Application.Basket.Commands.Request;
using BestDigiSellerApp.Basket.Application.Clients;
using BestDigiSellerApp.Basket.Application.Validaton;
using BestDigiSellerApp.Basket.Data.Abstract;
using BestDigiSellerApp.Basket.Data.Concrete;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace BestDigiSellerApp.Basket.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BasketDbConnection");
            services.AddDbContext<BasketContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.Basket.Api");
            }
           ));
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
        public static void CreateProductClient(this IServiceCollection services)
        {
            services.AddHttpClient<ProductClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-product-api");
            });
        }

        public static void CreateStripeClient(this IServiceCollection services)
        {
            services.AddHttpClient<StripeClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-stripe-api");
            });
        }

        public static void CreateDiscountClient(this IServiceCollection services)
        {
            services.AddHttpClient<StripeClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-discount-api");
            });
        }



        public static void ServiceLifetimeOptions(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<IValidator<CreateBasketCommandRequest>, BasketForCreateDtoValidator>();
            services.AddScoped<IValidator<DeleteItemToBasketCommandRequest>, DeleteItemToBasketDtoValidator>();
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });

            services.AddScoped<IBasketDal, BasketDal>();
        }
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
    }
}
