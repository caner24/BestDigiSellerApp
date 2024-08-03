using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Data.Concrete;
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
            services.AddDbContext<WalletContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BestDigiSellerApp.Wallet.Api")));
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



            var emailConfig = config
       .GetSection("EmailConfiguration")
       .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);
        }

    }
}
