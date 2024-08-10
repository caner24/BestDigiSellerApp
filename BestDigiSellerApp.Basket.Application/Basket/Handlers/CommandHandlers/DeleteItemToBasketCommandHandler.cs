using BestDigiSellerApp.Basket.Application.Basket.Commands.Request;
using BestDigiSellerApp.Basket.Data.Abstract;
using BestDigiSellerApp.Basket.Entity.Dto;
using BestDigiSellerApp.Basket.Entity.Result;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Basket.Handlers.CommandHandlers
{
    public record DeleteItemToBasketCommandHandler : IRequestHandler<DeleteItemToBasketCommandRequest, Result>
    {
        private readonly IBasketDal _bastketDal;
        private readonly ClaimsPrincipal _claimPrincipal;
        public DeleteItemToBasketCommandHandler(IBasketDal basketDal, ClaimsPrincipal claimPrincipal)
        {
            _claimPrincipal = claimPrincipal;
            _bastketDal = basketDal;
        }
        public async Task<Result> Handle(DeleteItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _claimPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).First().Value;
            var basket = await _bastketDal.Get(x => x.UserName == user).FirstOrDefaultAsync();
            if (basket is null)
                return Result.Fail(new BasketEmptyResult());
            var basketItems = basket.Items.Where(x => x.ProductId == request.ProductId).FirstOrDefault();
            if (request.Quantity<basketItems.Quantity)
                return Result.Fail(new BasketQuantityLessResult());

            basketItems.Quantity -= -request.Quantity;
            if (basketItems.Quantity == 0)
            {
                await _bastketDal.DeleteAsync(basket);
                return Result.Ok();
            }
            {
                await _bastketDal.UpdateAsync(basket);
                return Result.Ok();
            }
        }
    }
}
