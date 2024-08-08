using BestDigiSellerApp.Basket.Data.Abstract;
using BestDigiSellerApp.Basket.Entity;
using BestDigiSellerApp.Core.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Data.Concrete
{
    public class BasketDal : EFCoreRepositoryBase<BasketContext, ShoppingCart>, IBasketDal
    {
        public BasketDal(BasketContext tContext) : base(tContext)
        {

        }
    }
}
