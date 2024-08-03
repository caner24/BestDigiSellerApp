using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class UserLockedOutException : BaseException
    {
        public UserLockedOutException() : base("You've locked out.")
        {
        }
    }
}
