
namespace BestDigiSellerApp.User.Api
{
    public class WalletClient(HttpClient httpClient)
    {
        public async Task<WalletResponse> CreateWalletAsync(WalletRequest walletRequest, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync<WalletRequest>("/api/wallet/createWallet", walletRequest);

            return response.Content.ReadFromJsonAsync<WalletResponse>(cancellationToken: cancellationToken).Result;
        }
    }
}
public record WalletResponse
{
    public string? Iban { get; init; }
}
public record WalletRequest
{
    public string UserId { get; init; }
    public Currency Currency { get; init; }
}
public enum Currency
{
    TRY,
    EUR,
    USD
}
