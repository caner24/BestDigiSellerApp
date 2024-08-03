using BestDigiSellerApp.Core.Data;
using BestDigiSellerApp.Wallet.Entity;
using BestDigiSellerApp.Wallet.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Data.Abstract
{
    public interface IWalletDal : IEntityRepositoryBase<Wallet.Entity.Wallet>
    {

    }
}
