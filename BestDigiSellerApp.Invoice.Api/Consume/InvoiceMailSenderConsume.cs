using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Invoice.Entity.Dto;
using MassTransit;

namespace BestDigiSellerApp.Invoice.Api.Consume
{
    public class InvoiceMailSenderConsume : IConsumer<InvoiceMailSenderDto>
    {
        private readonly IEmailSender _emailSender;
        public InvoiceMailSenderConsume(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<InvoiceMailSenderDto> context)
        {
            _emailSender.SendEmail(new Message(new string[] { context.Message.MailAdress }, "Fieche Detail", $"<h4>Thaknks for your order fieche -> {context.Message.Fieche}</h4>", null));
            await Task.CompletedTask;
        }
    }
}
