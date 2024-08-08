using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Results
{
    public class ProductNotFoundResult : Error
    {
        public ProductNotFoundResult() : base("The product was not found")
        {

        }
    }
}
