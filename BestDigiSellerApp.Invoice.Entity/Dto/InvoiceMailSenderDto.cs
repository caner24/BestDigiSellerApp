using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Entity.Dto
{
    public record InvoiceMailSenderDto
    {
        public string MailAdress { get; init; }
        public string Fieche { get; init; }

    }
}
