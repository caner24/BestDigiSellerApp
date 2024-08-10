
using Asp.Versioning;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.User.Api.ActionFilters;
using BestDigiSellerApp.User.Api.Consume;
using BestDigiSellerApp.User.Application.User.Commands.Request;
using BestDigiSellerApp.User.Application.User.Queries.Request;
using BestDigiSellerApp.User.Application.Validation.FluentValidaton;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Data.Concrete;
using BestDigiSellerApp.User.Entity.Dto;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace BestDigiSellerApp.User.Api.Extensions
{
    public static class ServiceExtension
    {

        public static void IdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UserSqlConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.User.Api");

            }));
            services.AddIdentity<BestDigiSellerApp.User.Entity.User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<UserContext>();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromHours(2));
        }
        public static void CreateWalletClient(this IServiceCollection services)
        {
            services.AddHttpClient<WalletClient>(client =>
            {
                client.BaseAddress = new("https://bestdigisellerapp-wallet-api");
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
        public static void AddMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<CreateWalletConsume>();
                x.AddConsumer<EmailConfirmationConsumer>();
                x.AddConsumer<LoginTwoStepConsume>();
                x.AddConsumer<PasswordResetConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config.GetConnectionString("messaging");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("email-confirmation", e =>
                    {
                        e.ConfigureConsumer<EmailConfirmationConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("create-wallet-verification", e =>
                    {
                        e.ConfigureConsumer<CreateWalletConsume>(context);
                    });
                    cfg.ReceiveEndpoint("password-reset-token", e =>
                    {
                        e.ConfigureConsumer<PasswordResetConsumer>(context);
                    });
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
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<IUserDal, UserDal>();
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


            services.AddScoped<IValidator<LoginUserCommandRequest>, UserForLoginDtoValidator>();
            services.AddScoped<IValidator<CreateAdminCommandRequest>, AdminForAddDeletOrUpdateDtoValidator>();
            services.AddScoped<IValidator<ReConfirmMailCodeCommandRequest>, ReConfirmMailCodeDtoValidator>();
            services.AddScoped<IValidator<DeleteAdminCommandRequest>, AdminForAddDeletOrUpdateDtoValidator>();
            services.AddScoped<IValidator<ConfirmMailQueryRequest>, ConfirmMailQueryRequestDtoValidator>();
            services.AddScoped<IValidator<RegisterUserCommandRequest>, UserForRegistrationDtoValidator>();
            services.AddScoped<IValidator<LoginTwoStepCommandRequest>, TwoStepLoginDtoValidator>();
            services.AddScoped<IValidator<ForgottenPasswordCommandRequest>, ForgottonPasswordDtoValidator>();
            services.AddScoped<IValidator<PasswordResetCommandRequest>, UserPasswordResetDtoValidator>();
        }

    }
}
