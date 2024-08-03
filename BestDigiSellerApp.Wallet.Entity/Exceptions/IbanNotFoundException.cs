using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Exceptions
{
    public class IbanNotFoundException : BaseException
    {
        public IbanNotFoundException() : base("The iban was not found")
        {
        }
    }
}
