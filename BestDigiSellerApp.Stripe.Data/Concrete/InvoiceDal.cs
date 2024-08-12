using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Stripe.Data.Abstract;
using BestDigiSellerApp.Stripe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Data.Concrete
{
    public class InvoiceDal : EFCoreRepositoryBase<StripeContext, Invoice>, IInvoiceDal
    {

        public InvoiceDal(StripeContext tContext) : base(tContext)
        {
        }
    }
}
