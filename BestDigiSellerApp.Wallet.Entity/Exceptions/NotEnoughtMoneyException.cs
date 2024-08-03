using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Exceptions
{
    public class NotEnoughtMoneyException : BaseException
    {
        public NotEnoughtMoneyException() : base("Your current amount that want sent under than sent.")
        {
        }
    }
}
