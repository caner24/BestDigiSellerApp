using BestDigiSellerApp.Discount.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Data.Concrete
{
    public class UnitOfWorkDal : IUnitOfWorkDal
    {
        private readonly IDiscountDal _discountDal;
        private readonly IDiscountUserDal _discountUserDal;
        private readonly DiscountContext _discountContext;
        public UnitOfWorkDal(IDiscountDal discountDal, IDiscountUserDal discountUserDal, DiscountContext context)
        {
            _discountDal = discountDal;
            _discountUserDal = discountUserDal;
            _discountContext = context;
        }


        public IDiscountDal DiscountDal => _discountDal;

        public IDiscountUserDal DiscountUserDal => _discountUserDal;


        public DiscountContext Context => _discountContext;

        public IDiscountDal ProductDal => throw new NotImplementedException();

        public async Task BeginTransactionAsync()
        {
            if (_discountContext.Database.CurrentTransaction == null)
            {
                await _discountContext.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (_discountContext.Database.CurrentTransaction != null)
            {
                await _discountContext.Database.CurrentTransaction.CommitAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _discountContext.SaveChangesAsync();
        }
    }
}
