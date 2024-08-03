﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Exceptions
{
    public class EmailConfirmedException : BaseException
    {
        public EmailConfirmedException() : base("You've already confirmed your email adress !.")
        {
        }
    }
}
