using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Invoice.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Data.Concrete
{
    public class InvoiceDal:EFCoreRepositoryBase<InvoiceContext,Invoice.Entity.Invoice>,IInvoiceDal
    {

        public InvoiceDal(InvoiceContext tContext) : base(tContext)
        {
        }
    }
}
