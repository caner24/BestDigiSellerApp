﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record TwoStepLoginDto
    {
        public string Email { get; init; }
        public string Token { get; init; }
    }
}
