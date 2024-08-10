using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Result
{
    public class CheckoutError:Error
    {
        public CheckoutError():base("The checkout system is unavailable now. Please try again!")
        {
            
        }
    }
}
