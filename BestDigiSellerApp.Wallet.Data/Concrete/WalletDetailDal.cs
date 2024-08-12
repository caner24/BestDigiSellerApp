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
    }
}
