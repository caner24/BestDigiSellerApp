﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Commands.Response
{
    public record ForgottonPasswordCommandRepsonse
    {
        public string? Token { get; init; }
    }
}
