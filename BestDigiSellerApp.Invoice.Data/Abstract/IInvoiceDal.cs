using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Invoice.Data.Concrete;
using BestDigiSellerApp.Invoice.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Data.Abstract
{
    public interface IInvoiceDal : IEntityRepositoryBase<Invoice.Entity.Invoice>
    {

    }
}
