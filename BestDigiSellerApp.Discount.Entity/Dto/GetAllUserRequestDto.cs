using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Entity.Dto
{
    public record GetAllUserRequestDto
    {
        public string? Bearer { get; init; }
    }
}
