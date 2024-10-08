﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record ForgottonPasswordDto
    {
        public string? Email { get; init; }
        public string? Token { get; init; }
    }
}
