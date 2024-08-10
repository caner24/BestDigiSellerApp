using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Entity.Result
{
    public class SessionNotFound : Error
    {
        public SessionNotFound() : base("The session was not found")
        {

        }
    }
}
