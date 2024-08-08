using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity
{
    public class ShoppingCart : IEntity
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }
        public int Id { get; set; }
        public string? UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
