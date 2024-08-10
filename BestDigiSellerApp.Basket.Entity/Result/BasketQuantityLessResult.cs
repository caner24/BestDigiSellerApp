using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Result
{
    public class BasketQuantityLessResult:Error
    {
        public BasketQuantityLessResult():base("The basket quantity less than you've gived quantity !.")
        {
            
        }


    }
}
