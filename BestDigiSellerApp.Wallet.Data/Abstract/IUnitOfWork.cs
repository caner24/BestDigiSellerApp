using BestDigiSellerApp.Wallet.Data.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Abstract
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        IWalletDal WalletDal { get; }
        IWalletDetailDal WalletDetailDal { get; }
        ClaimsPrincipal ClaimsPrincipal { get; }
        WalletContext Context { get; }

    }
}
