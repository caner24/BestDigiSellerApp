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

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public bool ValidYearRange => MaxPrice >= MinPrice;
        public string Name { get; set; }
    }
}
