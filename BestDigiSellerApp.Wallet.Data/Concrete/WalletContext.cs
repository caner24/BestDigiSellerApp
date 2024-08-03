using BestDigiSellerApp.Wallet.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Concrete
{
    public class WalletContext : DbContext
    {
        public WalletContext()
        {

        }
        public WalletContext(DbContextOptions<WalletContext> options) : base(options)
        {
        }


        public DbSet<Wallet.Entity.Wallet> Wallet { get; set; }
        public DbSet<WalletDetail> WalletDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WalletDetail>().Property(x => x.Iban).HasMaxLength(35);
            base.OnModelCreating(modelBuilder);
        }
    }
}
