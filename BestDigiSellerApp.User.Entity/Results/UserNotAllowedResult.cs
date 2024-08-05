using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class UserNotAllowedResult : Error
    {
        public UserNotAllowedResult() : base("Password or email adress are wrong !.")
        {
        }
    }
}
