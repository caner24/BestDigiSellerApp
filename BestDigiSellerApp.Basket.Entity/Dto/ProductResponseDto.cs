using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Entity.Dto
{

    public record ProductResponseDto
    {
        public string id { get; init; }
        public string name { get; init; }
        public string concurrency { get; init; }
        public Photo[] photos { get; init; }
        public Productdetail productDetail { get; init; }
        public object[] categories { get; init; }
    }

    public record Productdetail
    {
        public string productId { get; init; }
        public double pointPercentage { get; init; }
        public double maxPoint { get; init; }
        public int stock { get; init; }
        public double price { get; init; }
    }

    public record Photo
    {
        public int id { get; init; }
        public string url { get; init; }
        public object[] products { get; init; }
    }

}
