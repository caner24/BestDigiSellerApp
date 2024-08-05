using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Wallet.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.Wallet.Api.Consume
{
    public class WalletCreatedEmailSenderConsumer : IConsumer<WalletCreatedDto>
    {
        private readonly IEmailSender _emailSender;
        public WalletCreatedEmailSenderConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<WalletCreatedDto> context)
        {
            Log.Information("Wallet info consume has been started.");
            _emailSender.SendEmail(new Message(new string[] { context.Message.UserEmail }, "Wallet Info", $"Your wallet has succesfully created. Your iban number is  -> {context.Message.Iban}", null));
            await Task.CompletedTask;
        }
    }
}
