using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Data.Abstract
{
    public interface IUnitOfWorkDal
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        IDiscountDal ProductDal { get; }
        IDiscountUserDal DiscountUserDal { get; }
    }
}
