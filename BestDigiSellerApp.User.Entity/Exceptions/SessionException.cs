using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class SessionException : BaseException
    {
        public SessionException() : base("Session was not found")
        {

        }
    }
}
