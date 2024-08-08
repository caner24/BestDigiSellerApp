using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Entity.Dto
{
    public record ProductForUpdateDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; init; }
        public double PointPercentage { get; init; }
        public double MaxPoint { get; init; }
        public int Stock { get; init; }
        public double Price { get; init; }
        public List<Guid> CategoryId { get; init; }
        public List<IFormFile> FormFiles { get; init; }
    }
}
