using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Commands.Response
{
    public record CreateCategoryCommandResponse
    {
        public Guid CategoryId { get; set; }
    }
}
