using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Result
{
    public class CouponIsNotValid : Error
    {
        public CouponIsNotValid() : base("The coupon code is not valid")
        {

        }
    }
}
