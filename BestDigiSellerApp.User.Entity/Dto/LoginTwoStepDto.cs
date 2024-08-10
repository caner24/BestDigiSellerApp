using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record LoginTwoStepDto
    {
        public string? Code { get; init; }
        public bool IsPersident { get; init; }
        public bool RememberClient { get; init; }
    }
}
