using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Discount.Commands.Response
{
    public record CreateCouponCodeCommandResponse
    {
        public string? CouponCode { get; init; }
        public string? EmailAdress { get; init; }
    }
}
