using Asp.Versioning;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Invoice.Api.Consume;
using BestDigiSellerApp.Invoice.Data.Abstract;
using BestDigiSellerApp.Invoice.Data.Concrete;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BestDigiSellerApp.Invoice.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void DbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("InvoiceDbConnection");
            services.AddDbContext<InvoiceContext>(options => options.UseSqlServer(connectionString, b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly("BestDigiSellerApp.Invoice.Api");
            }
           ));
        }
        public static void AddMassTransit(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<InvoiceMailSenderConsume>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config.GetConnectionString("messaging");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("coupon-mail-sender", e =>
                    {
                        e.ConfigureConsumer<InvoiceMailSenderConsume>(context);
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
            services.AddHttpContextAccessor();
        
            services.AddSingleton<IEmailSender, EmailSender>();
            var emailConfig = config
 .GetSection("EmailConfiguration")
 .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddScoped<IInvoiceDal, InvoiceDal>();

        }
    }
}
