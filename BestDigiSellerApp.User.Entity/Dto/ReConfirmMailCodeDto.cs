﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record ReConfirmMailCodeDto
    {
        public string EmailAdress { get; init; }
    }
}
