using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Entity.Results
{
    public class SessionNotValidResult : Error
    {
        public SessionNotValidResult() : base("Session is not valid")
        {
        }
    }
}
