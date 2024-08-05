using BestDigiSellerApp.Wallet.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Dto
{
    public record WalletCreatedDto
    {
        public string? Iban { get; init; }
        public string UserEmail { get; init; }
        public Currency Currency { get; set; }
    }
}
