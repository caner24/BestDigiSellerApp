using BestDigiSellerApp.Basket.Entity;
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
    public class StripeClient(HttpClient httpClient)
    {
        public async Task<Result<string>> CreateCheckout(ShoopingCartDto cartRequest, CancellationToken cancellationToken = default)
        {

            var accessToken = cartRequest.Bearer.Split(" ").Last();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.PostAsJsonAsync($"/api/Stripe/createCheckoutSessionForProduct", cartRequest, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail(new CheckoutError());
            }
            var stripeCheckout=await response.Content.ReadAsStringAsync();


            return Result.Ok(stripeCheckout);
        }
    }
}
