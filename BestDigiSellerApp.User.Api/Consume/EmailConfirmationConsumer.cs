using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.User.Api.Consume
{
    public class EmailConfirmationConsumer : IConsumer<EmailConfirmationDto>
    {
        private readonly IEmailSender _emailSender;
        public EmailConfirmationConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<EmailConfirmationDto> context)
        {
            _emailSender.SendEmail(new Message(new string[] { context.Message.EmailAdress }, "Email Confirmation", $"<h4>Your mail confirmation link -> {context.Message.ConfirmationLink}</h4>", null));
            await Task.CompletedTask;
        }
    }
}
