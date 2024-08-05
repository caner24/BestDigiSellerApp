using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class PasswordResetTokenExpireResult : Error
    {
        public PasswordResetTokenExpireResult(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
