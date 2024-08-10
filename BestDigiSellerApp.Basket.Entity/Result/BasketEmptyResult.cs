using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Result
{
    public class BasketEmptyResult : Error
    {
        public BasketEmptyResult() : base("The Product is empty.")
        {

        }
    }
}
