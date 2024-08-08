using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Result
{
    public class ProductNotFoundResult : Error
    {
        public ProductNotFoundResult() : base("The Product was not fund")
        {

        }
    }
}
