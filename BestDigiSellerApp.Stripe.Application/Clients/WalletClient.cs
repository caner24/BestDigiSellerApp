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
    public class WalletClient(HttpClient httpClient)
    {
        public async Task<Result> AddFundsToWallet(AddWalletFundsDto productRequest, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync<AddWalletFundsDto>($"/api/Wallet/addFundsToWallet", productRequest, cancellationToken);
            return Result.Ok();
        }
    }
}
