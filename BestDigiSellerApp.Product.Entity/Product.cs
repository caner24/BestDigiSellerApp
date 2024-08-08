using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity
{
    public class Product : IEntity
    {
        public Product()
        {
            Categories = new HashSet<Category>();
            Photos = new HashSet<Photo>();
        }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public byte[] Concurrency { get; set; }
        public HashSet<Photo> Photos { get; set; }
        public ProductDetail? ProductDetail { get; set; }
        public HashSet<Category> Categories { get; set; }
    }
}
