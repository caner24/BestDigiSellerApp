using BestDigiSellerApp.Stripe.Application.Clients;
using BestDigiSellerApp.Stripe.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.Stripe.Api.Consumer
{
    public class CashBackConsume : IConsumer<AddWalletFundsDto>
    {
        private readonly WalletClient _client;
        public CashBackConsume(WalletClient client)
        {
            _client = client;
        }
        public async Task Consume(ConsumeContext<AddWalletFundsDto> context)
        {
            Log.Information("CashBackConsume has been started . . .");
            await _client.AddFundsToWallet(context.Message);

            await Task.CompletedTask;
        }

    }
}

