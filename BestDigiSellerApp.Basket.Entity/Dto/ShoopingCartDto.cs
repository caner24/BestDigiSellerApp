using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Dto
{
    public class ShoopingCartDto
    {
        public ShoopingCartDto()
        {
            Products = new List<ProductDto>();
        }
        public string Bearer { get; set; }
        public string? EmailAdress { get; set; }
        public List<ProductDto>? Products { get; set; }
    }
}
