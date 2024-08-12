using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Entity.Dto
{
    public record CreateInvoiceDto
    {
        public string MailAdress { get; init; }
        public double Total { get; init; }
        public string ProductId { get; init; }
        public string CouponCode { get; init; }
        public int Amount { get; init; }
        public double CashBackBalance { get; init; }
    }
}
