using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Entity;
using BestDigiSellerApp.Wallet.Entity.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Concrete
{
    public class WalletDetailDal : EFCoreRepositoryBase<WalletContext, WalletDetail>, IWalletDetailDal
    {
        private readonly WalletContext _context;
        public WalletDetailDal(WalletContext tContext) : base(tContext)
        {
            _context = tContext;

        }

        //public async Task SendMoneyFastAsync(string iban, float amount)
        //{
        //    var currentUserId = _unitOfWork.ClaimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
        //    var currentUser = await _unitOfWork.WalletDetailDal.Get(x => x.Wallet.UserId == currentUserId).FirstOrDefaultAsync();
        //    var ibanUser = await _unitOfWork.WalletDetailDal.Get(x => x.Iban == iban).FirstOrDefaultAsync();

        //    if (ibanUser is null)
        //        throw new IbanNotFoundException();

        //    if (currentUser.Amount < amount)
        //        throw new NotEnoughtMoneyException();

        //    if (currentUser.Currency != ibanUser.Currency)
        //        throw new CurrenciesAreNotSameException();

        //    await _unitOfWork.BeginTransactionAsync();
        //    currentUser.Amount -= amount;
        //    ibanUser.Amount += amount;
        //    await _unitOfWork.Context.SaveChangesAsync();
        //    await _unitOfWork.CommitAsync();
        //}
    }
}
