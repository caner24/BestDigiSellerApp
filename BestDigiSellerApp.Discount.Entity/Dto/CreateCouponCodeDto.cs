using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Entity.Dto
{
    public record CreateCouponCodeDto
    {
        public string? Email { get; init; }
        public string? CouponCode { get; init; }
        public DateTime ExpireTime { get; init; }
        public int CouponPercentage { get; init; }
    }
}
