using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Product.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Concrete
{
    public class ProductDal : EFCoreRepositoryBase<ProductContext, Entity.Product>, IProductDal
    {
        public ProductDal(ProductContext tContext) : base(tContext)
        {
        }
    }
}
