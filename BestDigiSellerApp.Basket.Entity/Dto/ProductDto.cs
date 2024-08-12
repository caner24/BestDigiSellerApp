using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Dto
{
    public record ProductDto
    {
        public string ProductId { get; init; }
        public string Email { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public int Quantity { get; init; }
        public string Fieche { get; init; }
        public double CashBackAmount { get; init; }
    }
}
