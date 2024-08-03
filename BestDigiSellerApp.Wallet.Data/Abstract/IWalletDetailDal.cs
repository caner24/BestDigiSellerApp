using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Wallet.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Abstract
{
    public interface IWalletDetailDal : IEntityRepositoryBase<WalletDetail>
    {
        //Task SendMoneyFastAsync(string iban, float amount);
    }
}
