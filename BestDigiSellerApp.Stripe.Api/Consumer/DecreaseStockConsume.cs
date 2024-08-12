using BestDigiSellerApp.Stripe.Application.Clients;
using BestDigiSellerApp.Stripe.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.Stripe.Api.Consumer
{
    public class DecreaseStockConsume : IConsumer<DecreaseStockDto>
    {
        private readonly ProductClient _client;
        public DecreaseStockConsume(ProductClient client)
        {
            _client = client;
        }
        public async Task Consume(ConsumeContext<DecreaseStockDto> context)
        {
            Log.Information("DecreaseStockConsume has been started . . .");
            foreach (var item in context.Message.ProductAmountDto)
            {
                await _client.UpdateQuantity(item);
            }

            await Task.CompletedTask;
        }

    }
}
