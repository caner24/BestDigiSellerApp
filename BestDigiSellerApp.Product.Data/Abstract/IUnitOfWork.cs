using BestDigiSellerApp.Product.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Abstract
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        IProductDal ProductDal { get; }
        ICategoryDal CategoryDal { get; }
        ProductContext Context { get; }
    }
}
