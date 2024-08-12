using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity
{
    public class Invoice : IEntity
    {
        public Invoice()
        {
            InvoiceProductDetail = new List<InvoiceProductDetail>();
        }
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public string FiecheNo { get; set; }

        public string CouponCode { get; set; }

        public double CashbackAmount { get; set; }
        public double TotalPrice { get; set; }

        public List<InvoiceProductDetail> InvoiceProductDetail { get; set; }
    }
}
