
using BestDigiSellerApp.User.Entity.Dto;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BestDigiSellerApp.User.Api
{
    public class WalletClient(HttpClient httpClient)
    {
        public async Task<HttpResponseMessage> CreateWalletAsync(WalletRequestDto walletRequest, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Wallet/createWallet", walletRequest, cancellationToken);

            return response;
        }
    }
}