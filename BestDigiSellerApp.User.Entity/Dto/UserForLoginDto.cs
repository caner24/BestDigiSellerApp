﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record UserForLoginDto
    {
        public string? UserEmail { get; init; }
        public string? Password { get; init; }
        public bool IsRemember { get; set; }
    }
}
