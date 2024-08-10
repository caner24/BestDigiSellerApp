using BestDigiSellerApp.Stripe.Entity.Dto;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Application.Clients
{
    public class ProductClient(HttpClient httpClient)
    {
        public async Task<Result> UpdateQuantity(ProductDto productRequest, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync<ProductDto>($"/api/Product/productQuantityChange", productRequest, cancellationToken);
            return Result.Ok();
        }
    }
}
