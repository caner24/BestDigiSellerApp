using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Dto
{
    public record CreateCheckoutSessionForWalletDto
    {
        public const string Name = "Wallet";
        public long Price { get; init; }

        public const int Quantity = 1;
    }
}
