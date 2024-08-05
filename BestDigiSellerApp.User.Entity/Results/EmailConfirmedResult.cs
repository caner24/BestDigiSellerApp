using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class EmailConfirmedResult : Error
    {
        public EmailConfirmedResult() : base("You've already confirmed your email adress !.")
        {
        }
    }
}
