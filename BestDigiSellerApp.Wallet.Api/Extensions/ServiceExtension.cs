using Asp.Versioning;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Wallet.Api.Consume;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Data.Concrete;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BestDigiSellerApp.Wallet.Api.Extensions
{
    public static class ServiceExtension
    {

        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("WalletSqlConnection");
            services.AddDbContext<WalletContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.Wallet.Api");
            }
           ));
        }
        public static void AddMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<WalletCreatedEmailSenderConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config.GetConnectionString("messaging");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("created-wallet-consumer", e =>
                    {
                        e.ConfigureConsumer<WalletCreatedEmailSenderConsumer>(context);
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
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                return context?.HttpContext?.User ?? new ClaimsPrincipal();
            });
            services.AddScoped<IWalletDal, WalletDal>();
            services.AddScoped<IWalletDetailDal, WalletDetailDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEmailSender, EmailSender>();
            var emailConfig = config
       .GetSection("EmailConfiguration")
       .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);
        }

    }
}
