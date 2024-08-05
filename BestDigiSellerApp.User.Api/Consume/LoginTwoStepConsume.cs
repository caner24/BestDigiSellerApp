using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.User.Entity.Dto;
using MassTransit;

namespace BestDigiSellerApp.User.Api.Consume
{
    public class LoginTwoStepConsume : IConsumer<TwoStepLoginDto>
    {
        private readonly IEmailSender _emailSender;
        public LoginTwoStepConsume(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<TwoStepLoginDto> context)
        {
            _emailSender.SendEmail(new Message(new string[] { context.Message.Email }, "2FA Security Login Code", $"<h4>Your 2step code is -> {context.Message.Token}</h4>", null));
            await Task.CompletedTask;
        }
    }
}
