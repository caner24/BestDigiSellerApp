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
    public class DiscountClient(HttpClient httpClient)
    {
        public async Task<Result<int>> GetCoupon(ValidateCouponCodeDto discountRequest, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync<ValidateCouponCodeDto>($"/api/Product/getProductById", discountRequest, cancellationToken);
            if (!response.IsSuccessStatusCode)
                return Result.Fail(new CouponIsNotValid());

            var responseData = await response.Content.ReadFromJsonAsync<int>();
            return Result.Ok(responseData);

        }
    }
}
