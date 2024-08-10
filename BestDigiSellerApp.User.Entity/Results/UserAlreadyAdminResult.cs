using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Results
{
    public class UserAlreadyAdminResult : Error
    {
        public UserAlreadyAdminResult() : base("The user you've given role's already admin !.")
        {

        }
    }
}
