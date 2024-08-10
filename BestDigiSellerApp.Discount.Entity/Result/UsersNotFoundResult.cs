using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Entity.Result
{
    public class UsersNotFoundResult : Error
    {
        public UsersNotFoundResult() : base("Something went wrong when was fetching users")
        {

        }
    }
}
