using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Exceptions
{
    public class CurrenysAreNotSameResult : Error
    {
        public CurrenysAreNotSameResult() : base("The transfer money you've wanted currencies are not same !.")
        {
        }
    }
}
