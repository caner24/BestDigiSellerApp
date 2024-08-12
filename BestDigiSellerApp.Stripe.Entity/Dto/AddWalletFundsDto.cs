using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Dto
{
    public record AddWalletFundsDto
    {
        public double Amount { get; init; }
        public string? Email { get; init; }
    }
}
