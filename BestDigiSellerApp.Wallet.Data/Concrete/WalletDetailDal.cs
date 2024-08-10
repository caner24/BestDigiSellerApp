using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Entity;
using BestDigiSellerApp.Wallet.Entity.Exceptions;
using FluentResults;
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
        private readonly ClaimsPrincipal _claimPrincipal;
        public WalletDetailDal(WalletContext tContext, ClaimsPrincipal claimPrincipal) : base(tContext)
        {
            _context = tContext;
            _claimPrincipal = claimPrincipal;
        }

        public async Task<Result> SendMoneyFastAsync(string iban, float amount)
        {
            var currentUserId = _claimPrincipal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var currentUser = await _context.WalletDetail.Where(x => x.Wallet.UserId == currentUserId).FirstOrDefaultAsync();
            var ibanUser = await _context.WalletDetail.Where(x => x.Iban == iban).FirstOrDefaultAsync();

            if (ibanUser is null)
                return Result.Fail(new IbanNotFoundResult());

            if (currentUser.Amount < amount)
                return Result.Fail(new NotEnoughtMoneyResult());


            await _context.Database.BeginTransactionAsync();
            currentUser.Amount -= amount;
            ibanUser.Amount += amount;
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
            return Result.Ok();
        }
    }
}
