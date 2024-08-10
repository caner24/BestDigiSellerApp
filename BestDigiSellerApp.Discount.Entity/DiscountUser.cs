using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Entity
{
    public class DiscountUser : IEntity
    {
        public DiscountUser()
        {
            Discounts = new List<Discount>();
        }
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public bool WasItUsed { get; set; }
        public List<Discount> Discounts { get; set; }
    }
}
