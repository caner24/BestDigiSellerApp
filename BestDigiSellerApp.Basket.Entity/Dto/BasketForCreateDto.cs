using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Dto
{
    public record BasketForCreateDto
    {
        public string? UserName { get; init; }
        public int Quantity { get; init; }
        public double Price { get; init; }
        public string? ProductId { get; init; }
        public string? ImageFile { get; init; }
        public double MaxPoint { get; init; }
        public double PointPercentage { get; init; }
        public string? ProductName { get; init; }
    }
}
