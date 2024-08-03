using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.User.Api.Consume
{
    public class CreateWalletConsume : IConsumer<WalletRequest>
    {
        private readonly WalletClient _client;
        public CreateWalletConsume(WalletClient client)
        {
            _client = client;
        }
        public async Task Consume(ConsumeContext<WalletRequest> context)
        {
            Log.Information("CreateWalletConsume has started . . .");
            var ibanAdress = await _client.CreateWalletAsync(context.Message);
            Log.Information($"CreateWalletConsume has fisihed. Iban adress ->{ibanAdress.Iban} . . .");
        }
    }
}
