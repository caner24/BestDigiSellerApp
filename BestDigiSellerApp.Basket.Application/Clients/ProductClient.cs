using BestDigiSellerApp.Basket.Entity.Dto;
using BestDigiSellerApp.Basket.Entity.Result;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Clients
{
    public class ProductClient(HttpClient httpClient)
    {
        public async Task<Result<ProductResponseDto>> GetProduct(ProductRequestDto photosRequest, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetFromJsonAsync<ProductResponseDto>($"/api/Product/getProductById/{photosRequest.ProductId}", cancellationToken);
            if (response is null)
                return Result.Fail(new ProductNotFoundResult());
            return Result.Ok(response);

        }
    }
}
