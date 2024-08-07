using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity
{
    public class Photo : IEntity
    {
        public Photo()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string? Url { get; set; }
        public HashSet<Product> Products { get; set; }
    }
}
