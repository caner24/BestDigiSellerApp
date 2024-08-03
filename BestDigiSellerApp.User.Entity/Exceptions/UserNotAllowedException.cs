using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class UserNotAllowedException : BaseException
    {
        public UserNotAllowedException() : base("Password or email adress are wrong !.")
        {
        }
    }
}
