using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Data.Concrete
{
    public class BasketContext : DbContext
    {

        public BasketContext(DbContextOptions<BasketContext> options) : base(options)
        {

        }

        public BasketContext()
        {

        }

        public DbSet<Basket.Entity.ShoppingCart> ShoppingCart { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
