using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Dto
{
    public record AddedFileDto
    {
        public string File { get; init; }
        public string Path { get; init; }
        public long Size { get; init; }
    }
}
