﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record EmailConfirmationDto
    {
        public string EmailAdress { get; init; }
        public string ConfirmationLink { get; init; }
    }
}
