using BestDigiSellerApp.Stripe.Application.Stripe.Commands.Request;
using FluentResults;
using MediatR;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Stripe.Application.Stripe.Handlers.CommandHandlers
{
    public class CreateCheckoutSessionForProductCommandHandler : IRequestHandler<CreateCheckoutSessionForProductCommandRequest, Result<string>>
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public CreateCheckoutSessionForProductCommandHandler(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }
        public async Task<Result<string>> Handle(CreateCheckoutSessionForProductCommandRequest request, CancellationToken cancellationToken)
        {
            var domain = "https://localhost:7155";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                },
                Mode = "payment",
                SuccessUrl = domain + "/api/Stripe/success",
                CancelUrl = domain + "/api/Stripe/cancel",
            };

            foreach (var product in request.Products)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = product.Price,
                        Currency = "TRY",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                        },
                    },
                    Quantity = product.Quantity,
                });
            }
            options.CustomerEmail = request.EmailAdress;
            var service = new SessionService();
            Session session = service.Create(options);
            return await Task.Run(() => Result.Ok(session.Url));
        }
    }
}
