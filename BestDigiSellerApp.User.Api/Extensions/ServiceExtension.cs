
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.User.Api.Consume;
using BestDigiSellerApp.User.Data.Abstract;
using BestDigiSellerApp.User.Data.Concrete;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace BestDigiSellerApp.User.Api.Extensions
{
    public static class ServiceExtension
    {

        public static void IdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UserSqlConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BestDigiSellerApp.User.Api")));
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
            services.AddAuthorizationBuilder();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromHours(2));
        }
        public static void CreateWalletClient(this IServiceCollection services)
        {
            services.AddHttpClient<WalletClient>(client =>
            {
                client.BaseAddress = new("https+http://bestdigisellerapp-wallet-api");
            });
        }
        public static void AddMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<CreateWalletConsume>();
                x.AddConsumer<EmailConfirmationConsumer>();
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
                });
            });
        }
        public static void ServiceLifetimeOptions(this IServiceCollection services, IConfiguration config)
        {
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
        }

    }
}
