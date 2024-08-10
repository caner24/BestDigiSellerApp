using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;

namespace BestDigiSellerApp.User.Api.Consume
{
    public class PasswordResetConsumer : IConsumer<ForgottonPasswordDto>
    {
        private readonly IEmailSender _emailSender;
        public PasswordResetConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<ForgottonPasswordDto> context)
        {
            _emailSender.SendEmail(new Message(new string[] { context.Message.Email }, "Password Reset Token", $"<h4>Your password reset token code is  -> {context.Message.Token}</h4>", null));
            await Task.CompletedTask;
        }
    }
}
