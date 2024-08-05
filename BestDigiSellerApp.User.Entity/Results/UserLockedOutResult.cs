using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class UserLockedOutResult : Error
    {
        public UserLockedOutResult() : base("You've locked out.")
        {
        }
    }
}
