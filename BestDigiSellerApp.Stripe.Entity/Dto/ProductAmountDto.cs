using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Dto
{
    public class ProductAmountDto
    {
        public string? ProductId { get; init; }

        public int Quantity { get; init; }
    }
}
