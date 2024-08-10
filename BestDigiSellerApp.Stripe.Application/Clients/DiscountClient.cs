using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Application.Clients
{
    public class DiscountClient(HttpClient httpClient)
    {
        //public async Task<Result> UseCouponCode(ProductDto productRequest, CancellationToken cancellationToken = default)
        //{
        //    var response = await httpClient.PostAsJsonAsync<ProductDto>($"/api/Product/productQuantityChange", productRequest, cancellationToken);
        //    return Result.Ok();
        //}
    }
}
