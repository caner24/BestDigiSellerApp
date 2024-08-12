using BestDigiSellerApp.Stripe.Application.Stripe.Commands.Request;
using BestDigiSellerApp.Stripe.Data.Abstract;
using BestDigiSellerApp.Stripe.Entity;
using BestDigiSellerApp.Stripe.Entity.Dto;
using BestDigiSellerApp.Stripe.Entity.Result;
using FluentResults;
using MassTransit;
using MediatR;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Application.Stripe.Handlers.CommandHandlers
{
    public class CreateSuccessBillingCommandHandler : IRequestHandler<CreateSuccessBillingCommandRequest, Result>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IInvoiceDal _invoiceDal;
        public CreateSuccessBillingCommandHandler(IPublishEndpoint publishEndpoint, IInvoiceDal invoiceDal)
        {
            _publishEndpoint = publishEndpoint;
            _invoiceDal = invoiceDal;
        }
        public async Task<Result> Handle(CreateSuccessBillingCommandRequest request, CancellationToken cancellationToken)
        {
            var service = new SessionService();
            try
            {
                Session session = service.Get(request.session_id);
                var products = new List<ProductDto>();
                double cashBackAmount = 0;
                DecreaseStockDto decreaseStockDto = new DecreaseStockDto();
                decreaseStockDto.ProductAmountDto = new List<ProductAmountDto>();
                List<InvoiceProductDetail> pd = new List<InvoiceProductDetail>();
                var email = session.Metadata["emailadress"];
                double totalPrice = 0;
                int index = 0;
                while (session.Metadata.ContainsKey($"productid_{index}"))
                {
                    var productId = session.Metadata[$"productid_{index}"];
                    var quantity = session.Metadata[$"quantity_{index}"];
                    var price = session.Metadata[$"price_{index}"];
                    pd.Add(new InvoiceProductDetail { ProductAmount = Convert.ToDouble(price), ProductId = productId, ProductQuantity = quantity });
                    totalPrice += Convert.ToDouble(price);
                    decreaseStockDto.ProductAmountDto.Add(new ProductAmountDto { ProductId = productId, Quantity = Convert.ToInt32(quantity) });
                    // Only add cashback amount if it exists
                    if (session.Metadata.ContainsKey($"cashbackamount_{index}"))
                    {
                        cashBackAmount += Convert.ToDouble(session.Metadata[$"cashbackamount_{index}"]);
                    }
                    index++;
                }

                if (session.Metadata.ContainsKey("couponCode"))
                {
                    var coupon = session.Metadata["couponCode"];
                    var response = new
                    {
                        CouponCode = coupon,
                        Products = products
                    };
                    await _publishEndpoint.Publish<ProductDto>(new ProductDto { CashBackAmount = 0, Email = email, Fieche = GenerateOrderCode() });
                    await _publishEndpoint.Publish<DecreaseStockDto>(decreaseStockDto);
                    await _invoiceDal.AddAsync(new Entity.Invoice { CashbackAmount = cashBackAmount, CouponCode = coupon, FiecheNo = GenerateOrderCode(), UserEmail = email, TotalPrice = totalPrice, InvoiceProductDetail = pd });
                    return Result.Ok();
                }
                else
                {
                    var response = new
                    {
                        CashBackAmount = cashBackAmount,
                        Products = products
                    };
                    await _publishEndpoint.Publish<ProductDto>(new ProductDto { CashBackAmount = cashBackAmount, Email = email, Fieche = GenerateOrderCode() });
                    await _publishEndpoint.Publish<DecreaseStockDto>(decreaseStockDto);
                    if (cashBackAmount > 0)
                    {
                        await _publishEndpoint.Publish<AddWalletFundsDto>(new AddWalletFundsDto { Amount = cashBackAmount, Email = email });
                    }

                    await _invoiceDal.AddAsync(new Entity.Invoice { CashbackAmount = 0, CouponCode = "", FiecheNo = GenerateOrderCode(), UserEmail = email, TotalPrice = totalPrice, InvoiceProductDetail = pd });
                    return Result.Ok();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private static string GenerateOrderCode() =>
      new string(Enumerable.Range(0, new Random().Next(7, 10))
                           .Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[new Random().Next(36)])
                           .ToArray());
    }
}
