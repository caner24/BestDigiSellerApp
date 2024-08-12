using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Concrete.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product.Entity.Product>
    {
        public void Configure(EntityTypeBuilder<Entity.Product> builder)
        {

            builder.Navigation(x => x.ProductDetail).AutoInclude();
            builder.Navigation(x => x.Photos).AutoInclude();
        }
    }
}
