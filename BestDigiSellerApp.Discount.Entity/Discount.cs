using BestDigiSellerApp.Core.Data;
using System.Text;

namespace BestDigiSellerApp.Discount.Entity
{
    public class Discount : IEntity
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public DateTime ExpireTime { get; set; }
        public int CouponPercentage { get; set; }

        public int DiscountUserId { get; set; }
        public DiscountUser DiscountUser { get; set; }

    }
}
