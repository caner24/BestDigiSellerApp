using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Dto
{
    public record DeleteItemToBasketDto
    {
        public string ProductId { get; init; }
        public int Quantity { get; init; }
    }

}
