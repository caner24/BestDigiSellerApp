using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Entity.Dto
{
    public record ValidateCouponCodeDto
    {
        public string? Email { get; init; }
        public string? CouponCode { get; init; }
    }
}
