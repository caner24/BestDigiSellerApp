using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Wallet.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity
{
    public class WalletDetail : IEntity
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public string? Iban { get; set; }
        public Wallet Wallet { get; set; }
        public Currency Currency { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate => DateTime.UtcNow;

    }
}
