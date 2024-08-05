using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Exceptions
{
    public class IbanNotFoundResult : Error
    {
        public IbanNotFoundResult() : base("The iban was not found")
        {
        }
    }
}
