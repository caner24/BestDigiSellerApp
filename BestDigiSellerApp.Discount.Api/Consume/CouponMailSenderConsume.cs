using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Abstract;
using BestDigiSellerApp.Core.CrossCuttingConcerns.Email.Concrete;
using BestDigiSellerApp.Discount.Application.Client;
using BestDigiSellerApp.Discount.Entity.Dto;
using MassTransit;
using Serilog;

namespace BestDigiSellerApp.Discount.Api.Consume
{
    public class CouponMailSenderConsume : IConsumer<CreateCouponCodeDtoList>
    {
        private readonly UserClient _client;
        private readonly IEmailSender _emailSender;

        public CouponMailSenderConsume(UserClient client, IEmailSender emailSender)
        {
            _client = client;
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<CreateCouponCodeDtoList> context)
        {
            Log.Information("CouponMailSenderConsume has been started . . .");

            foreach (var item in context.Message.CouponCodes)
            {
                _emailSender.SendEmail(new Message(
                    new string[] { item.Email },
                    "Coupon Code",
                    $"<h4>You've earned a coupon code %{item.CouponPercentage} , Expire Time -> {item.ExpireTime}, Coupon Code : <b>{item.CouponCode}</b></h4>",
                    null
                ));
            }

            await Task.CompletedTask;
        }
    }
}
