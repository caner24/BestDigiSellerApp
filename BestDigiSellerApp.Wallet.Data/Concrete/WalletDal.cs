using BestDigiSellerApp.Core.Data.EntityFramework;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Entity;
using BestDigiSellerApp.Wallet.Entity.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Concrete
{
    public class WalletDal : EFCoreRepositoryBase<WalletContext, Wallet.Entity.Wallet>, IWalletDal
    {
        public WalletDal(WalletContext tContext) : base(tContext)
        {

        }
    }
}
