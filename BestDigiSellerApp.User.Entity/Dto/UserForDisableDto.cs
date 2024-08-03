using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record UserForDisableDto
    {
        public string? UserEmail { get; init; }
    }
}
