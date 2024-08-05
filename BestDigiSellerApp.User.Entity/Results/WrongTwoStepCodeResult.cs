
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class WrongTwoStepCodeResult : Error
    {
        public WrongTwoStepCodeResult() : base("Token you've entered is wrong !.")
        {

        }
    }
}
