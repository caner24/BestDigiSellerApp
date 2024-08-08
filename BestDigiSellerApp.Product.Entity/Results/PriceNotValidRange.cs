using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Results
{
    public class PriceNotValidRange:Error
    {

        public PriceNotValidRange():base("The price you've entered is not a valid price !")
        {
            
        }

    }
}
