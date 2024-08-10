using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Data.Concrete
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DiscountContext()
        {

        }

        public DbSet<Discount.Entity.Discount> Discount { get; set; }

    }
}
