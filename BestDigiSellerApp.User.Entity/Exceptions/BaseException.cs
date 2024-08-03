using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
