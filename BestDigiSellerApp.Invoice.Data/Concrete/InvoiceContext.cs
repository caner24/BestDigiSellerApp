using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Data.Concrete
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {

        }
        public InvoiceContext()
        {
            
        }
     

        public DbSet<Invoice.Entity.Invoice> Invoice { get; set; }
    }
}
