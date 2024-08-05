using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.User.Api.Consume
{
    public class CreateWalletConsume : IConsumer<WalletRequestDto>
    {
        private readonly WalletClient _client;
        public CreateWalletConsume(WalletClient client)
        {
            _client = client;
        }
        public async Task Consume(ConsumeContext<WalletRequestDto> context)
        {
            Log.Information("CreateWalletConsume has been started . . .");
            await _client.CreateWalletAsync(context.Message);
            await Task.CompletedTask;
        }
    }
}
