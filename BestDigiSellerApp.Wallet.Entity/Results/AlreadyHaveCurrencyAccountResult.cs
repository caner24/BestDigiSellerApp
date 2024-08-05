using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Exceptions
{
    public class AlreadyHaveCurrencyAccountResult : Error
    {
        public AlreadyHaveCurrencyAccountResult() : base("You've already has account this currency")
        {

        }
    }
}
