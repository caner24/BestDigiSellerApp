using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
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
            Log.Information($"Confirmation link {context.Message.ConfirmationLink}");
        }
    }
}
