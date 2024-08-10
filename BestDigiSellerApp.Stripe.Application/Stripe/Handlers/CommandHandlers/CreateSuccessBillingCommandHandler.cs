using BestDigiSellerApp.Stripe.Application.Stripe.Commands.Request;
using BestDigiSellerApp.Stripe.Entity.Dto;
using BestDigiSellerApp.Stripe.Entity.Result;
using FluentResults;
using MediatR;
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
        public async Task<Result> Handle(CreateSuccessBillingCommandRequest request, CancellationToken cancellationToken)
        {
            var service = new SessionService();
            Session session = service.Get(request.Session_id);
            if (session is null)
                return await Task.Run(() => Result.Fail(new SessionNotFound()));

            var products = new List<ProductDto>();

            for (int i = 0; i < session.Metadata.Count / 2; i++)
            {
                var productId = session.Metadata[$"productid_{i}"];
                var quantity = session.Metadata[$"quantity_{i}"];
                products.Add(new ProductDto
                {
                    ProductId = productId,
                    Quantity = int.Parse(quantity)
                });
            }
            var prod = products;
            return Result.Ok();
        }
    }
}
