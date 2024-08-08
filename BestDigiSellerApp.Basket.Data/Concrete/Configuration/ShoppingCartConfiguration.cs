using BestDigiSellerApp.Basket.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Data.Concrete.Configuration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Navigation(e => e.Items).AutoInclude();
            builder.HasIndex(x => x.UserName).IsUnique();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            builder.Property(x => x.Items)
          .HasConversion(
              v => JsonConvert.SerializeObject(v, serializerSettings),
              v => JsonConvert.DeserializeObject<List<ShoppingCartItem>>(v),
              new ValueComparer<List<ShoppingCartItem>>(
                  (c1, c2) => c1.SequenceEqual(c2),
                  c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                  c => new List<ShoppingCartItem>(c))
          );
        }
    }
}
