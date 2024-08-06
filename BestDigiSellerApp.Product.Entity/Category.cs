using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity
{
    public class Category: IEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Tag { get; set; }

        public HashSet<Product> Products { get; set; }
    }
}
