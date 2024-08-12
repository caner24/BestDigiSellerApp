using BestDigiSellerApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestDigiSellerApp.Stripe.Entity;
namespace BestDigiSellerApp.Stripe.Data.Abstract
{
    public interface IInvoiceDal : IEntityRepositoryBase<Invoice>
    {
    }
}
