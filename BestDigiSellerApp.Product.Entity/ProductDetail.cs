using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity
{
    public class ProductDetail: IEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public double PointPercentage { get; set; }
        public double MaxPoint { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public double CalculatePoints(int amount)
        {
            double pointsPerItem = Price * PointPercentage / 100;
            double totalPoints = pointsPerItem * amount;
            double maxPointsPerItem = MaxPoint * amount;
            return totalPoints > maxPointsPerItem ? maxPointsPerItem : totalPoints;
        }
    }
}
