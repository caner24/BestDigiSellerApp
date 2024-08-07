using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Dto
{
    public record PhotosForCreateDto
    {
        public List<IFormFile> Files { get; init; }
        public string Bearer { get; init; }
    }
}
