using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class EmailTokenExpireResult : Error
    {
        public EmailTokenExpireResult() : base("Your token is expired")
        {
        }
    }
}
