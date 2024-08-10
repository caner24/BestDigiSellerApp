using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Dto
{
    public record ProductQuantityChangeDto
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
    }
}
