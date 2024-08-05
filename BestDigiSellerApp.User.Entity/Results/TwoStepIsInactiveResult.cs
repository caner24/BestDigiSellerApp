using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class TwoStepIsInactiveResult : Error
    {
        public TwoStepIsInactiveResult() : base("Two step has not been actived.")
        {
        }
    }
}
