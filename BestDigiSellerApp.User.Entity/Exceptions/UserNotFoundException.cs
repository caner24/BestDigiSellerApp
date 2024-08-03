using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException() : base("User you searched was not found")
        {

        }
    }
}
