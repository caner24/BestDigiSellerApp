using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? ProductId { get; set; }
        public string? ImageFile { get; set; }
        public double MaxPoint { get; set; }
        public double PointPercentage { get; set; }
        public string? ProductName { get; set; }

    }
}
