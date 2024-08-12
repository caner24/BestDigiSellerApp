using BestDigiSellerApp.Stripe.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Data.Concrete
{
    public class StripeContext : DbContext
    {
        public StripeContext(DbContextOptions<StripeContext> options) : base(options)
        {

        }
        public StripeContext()
        {

        }


        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceProductDetail> InvoiceProductDetail { get; set; }

    }
}
