using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Dto
{
    public record CategoryForCreateDto
    {
        public string? Name { get; init; }
        public string? Tag { get; init; }
    }
}
