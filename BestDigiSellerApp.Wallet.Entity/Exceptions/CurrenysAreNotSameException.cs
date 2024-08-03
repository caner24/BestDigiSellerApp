using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Exceptions
{
    public class CurrenciesAreNotSameException : BaseException
    {
        public CurrenciesAreNotSameException() : base("The transfer money you've wanted currencies are not same !.")
        {
        }
    }
}
