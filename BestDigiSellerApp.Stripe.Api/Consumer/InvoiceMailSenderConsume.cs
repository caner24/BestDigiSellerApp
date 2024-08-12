using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Stripe.Entity.Dto;
using MassTransit;

namespace BestDigiSellerApp.Stripe.Api.Consumer
{
    public class InvoiceMailSenderConsume : IConsumer<ProductDto>
    {
        private readonly IEmailSender _emailSender;
        public InvoiceMailSenderConsume(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<ProductDto> context)
        {
            _emailSender.SendEmail(new Message(new string[] { context.Message.Email }, "Fieche Detail", $"<h4>Your invoice has succesfully paid. Fieche -> {context.Message.Fieche}, cashback earned for this product -> {context.Message.CashBackAmount}</h4>", null));
            await Task.CompletedTask;
        }
    }
}
