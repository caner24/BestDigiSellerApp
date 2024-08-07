using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Concrete
{
    public class CategoryDal : EFCoreRepositoryBase<ProductContext, Category>, ICategoryDal
    {
        public CategoryDal(ProductContext tContext) : base(tContext)
        {
        }
    }
}
