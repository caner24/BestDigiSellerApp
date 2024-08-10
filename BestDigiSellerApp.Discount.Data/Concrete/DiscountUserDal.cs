using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Discount.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Data.Concrete
{
    public class DiscountUserDal : EFCoreRepositoryBase<DiscountContext, Discount.Entity.DiscountUser>, IDiscountUserDal
    {
        public DiscountUserDal(DiscountContext tContext) : base(tContext)
        {

        }
    }
}
