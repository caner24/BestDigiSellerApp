using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Helpers
{
    public class ProductParameters : QueryStringParameters
    {
        public ProductParameters()
        {
            OrderBy = "Name";
        }

        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public bool ValidPriceRange => MaxPrice >= MinPrice;
        public string? Name { get; set; }
    }
}
