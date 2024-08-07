using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Product.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Abstract
{
    public interface IProductDal : IEntityRepositoryBase<Product.Entity.Product>
    {
    }
}
