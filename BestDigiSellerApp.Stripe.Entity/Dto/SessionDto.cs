using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Dto
{
    public record SessionDto
    {
        public string session_id { get; init; }
    }
}
