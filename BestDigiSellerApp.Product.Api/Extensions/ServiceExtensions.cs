
using Asp.Versioning;
using BestDigiSellerApp.Product.Api.ActionFilters;
using BestDigiSellerApp.Product.Application.Clients;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Validation.FluentValidation;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Data.Concrete;
using BestDigiSellerApp.Product.Entity;
using BestDigiSellerApp.Product.Entity.Dto;
using BestDigiSellerApp.Product.Entity.Helpers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace BestDigiSellerApp.Product.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProductDbConnection");
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.Product.Api");
            }
           ));
        }

        public static void ConfigureRedisOutputCache(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisOutputCache(_ => _.InstanceName = config["redis"]);
        }
        public static void CreateFileClient(this IServiceCollection services)
        {
            services.AddHttpClient<FileClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-file-api");
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

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                 .WithExposedHeaders("X-Pagination")

                );
            });
        }
        public static void ServiceLifetimeOptions(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });

            services.AddScoped<IDataShaper<BestDigiSellerApp.Product.Entity.Product>, DataShaper<BestDigiSellerApp.Product.Entity.Product>>();
            services.AddScoped<ISortHelper<BestDigiSellerApp.Product.Entity.Product>, SortHelper<BestDigiSellerApp.Product.Entity.Product>>();
            services.AddScoped<IDataShaper<Category>, DataShaper<Category>>();
            services.AddScoped<ISortHelper<Category>, SortHelper<Category>>();


            services.AddScoped<IValidator<CreateProductCommandRequest>, CreateProductDtoValidator>();
            services.AddScoped<IValidator<CreateCategoryCommandRequest>, CreateCategoryDtoValidator>();
            services.AddScoped<IValidator<DeleteProductCommandRequest>, DeleteProductDtoValidator>();
            services.AddScoped<IValidator<UpdateProductCommandRequest>, UpdateProductDtoValidator>();
            services.AddScoped<IValidator<DeleteCategoryCommandRequest>, DeleteCategoryDtoValidator>();
            services.AddScoped<IValidator<UpdateCategoryCommandRequest>, UpdateCategoryDtoValidator>();

            services.AddScoped<IProductDal, ProductDal>();
            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
