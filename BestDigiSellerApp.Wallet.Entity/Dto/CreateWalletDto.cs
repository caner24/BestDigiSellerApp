using BestDigiSellerApp.Wallet.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Dto
{
    public record CreateWalletDto
    {
        public string UserEmail { get; init; }
        public Currency Currency { get; init; }
    }
}
