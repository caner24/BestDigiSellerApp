using BestDigiSellerApp.Core.Data;

namespace BestDigiSellerApp.Invoice.Entity
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public string ProductId { get; set; }

        public double ProductAmount { get; set; }

        public string FiecheNo { get; set; }

        public string CouponCode { get; set; }

        public double CashbackAmount { get; set; }
    }
}
