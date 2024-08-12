using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity
{
    public class InvoiceProductDetail
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public double ProductAmount { get; set; }
        public string ProductQuantity { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
