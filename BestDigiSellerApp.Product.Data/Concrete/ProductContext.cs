using BestDigiSellerApp.Product.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Concrete
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public ProductContext()
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<Product.Entity.Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
