using Asp.Versioning;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Discount.Api.ActionFilters;
using BestDigiSellerApp.Discount.Api.Consume;
using BestDigiSellerApp.Discount.Application.Client;
using BestDigiSellerApp.Discount.Application.Discount.Commands.Request;
using BestDigiSellerApp.Discount.Application.Validation.FluentValidaton;
using BestDigiSellerApp.Discount.Data.Abstract;
using BestDigiSellerApp.Discount.Data.Concrete;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BestDigiSellerApp.Discount.Api.Extensions
{
    public static class ServiceExtension
    {

        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DiscountDbConnection");
            services.AddDbContext<DiscountContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.Discount.Api");
            }
           ));
        }

        public static void AddMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<CouponMailSenderConsume>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config.GetConnectionString("messaging");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("coupon-mail-sender", e =>
                    {
                        e.ConfigureConsumer<CouponMailSenderConsume>(context);
                    });
                });
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
        public static void CreateUserClient(this IServiceCollection services)
        {
            services.AddHttpClient<UserClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-user-api");
            });
        }
        public static void ServiceLifetimeOptions(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });
            services.AddSingleton<IEmailSender, EmailSender>();
            var emailConfig = config
 .GetSection("EmailConfiguration")
 .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);
            services.AddScoped<IValidator<CreateCouponCodeCommandRequest>, CreateCouponCodeDtoValidator>();
            services.AddScoped<IValidator<ValidateCouponCodeCommandRequest>, ValidateCouponCodeDtoValidator>();

            services.AddScoped<IDiscountDal, DiscountDal>();
            services.AddScoped<IDiscountUserDal, DiscountUserDal>();
            services.AddScoped<IUnitOfWorkDal, UnitOfWorkDal>();

        }





    }
}
