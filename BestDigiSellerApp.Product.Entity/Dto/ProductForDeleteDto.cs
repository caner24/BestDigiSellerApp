using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Dto
{
    public record ProductForDeleteDto
    {
        public Guid ProductId { get; init; }
    }
}
