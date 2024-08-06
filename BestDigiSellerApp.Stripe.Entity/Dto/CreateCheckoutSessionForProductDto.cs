using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Dto
{
    public record CreateCheckoutSessionForProductDto
    {
        public string EmailAdress { get; set; }
        public List<ProductDto>? Products { get; init; }
    }
}
