using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Dto
{
    public record ProductDto
    {
        public string Name { get; init; }
        public long Price { get; init; }
        public int Quantity { get; init; }
    }
}
