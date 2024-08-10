using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Discount.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Data.Concrete
{
    public class DiscountDal : EFCoreRepositoryBase<DiscountContext, Discount.Entity.Discount>, IDiscountDal
    {
        public DiscountDal(DiscountContext tContext) : base(tContext)
        {

        }
    }
}
