using BestDigiSellerApp.Product.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IProductDal _productDal;
        private readonly ICategoryDal _categoryDal;
        private readonly ProductContext _productContext;
        public UnitOfWork(IProductDal productDal, ICategoryDal categoryDal, ProductContext context)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
            _productContext = context;
        }


        public IProductDal ProductDal => _productDal;

        public ICategoryDal CategoryDal => _categoryDal;


        public ProductContext Context => _productContext;

        public async Task BeginTransactionAsync()
        {
            if (_productContext.Database.CurrentTransaction == null)
            {
                await _productContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (_productContext.Database.CurrentTransaction != null)
            {
                await _productContext.Database.CurrentTransaction.CommitAsync();
            }
        }
    }
}
