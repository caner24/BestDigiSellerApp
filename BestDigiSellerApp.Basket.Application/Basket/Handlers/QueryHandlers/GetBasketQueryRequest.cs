using AutoMapper;
using BestDigiSellerApp.Basket.Application.Basket.Queries.Request;
using BestDigiSellerApp.Basket.Application.Clients;
using BestDigiSellerApp.Basket.Data.Abstract;
using BestDigiSellerApp.Basket.Entity;
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

namespace BestDigiSellerApp.Basket.Application.Basket.Handlers.CommandResponse
{
    public class GetBasketQueryRequest : IRequestHandler<GetUserBasketQueryRequest, Result<ShoppingCart>>
    {
        private readonly IBasketDal _basketDal;
        private readonly ProductClient _client;
        private readonly ClaimsPrincipal _claimPrincipal;
        public GetBasketQueryRequest(IBasketDal basketDal, ProductClient client, ClaimsPrincipal claimPrincipal)
        {
            _basketDal = basketDal;
            _client = client;
            _claimPrincipal = claimPrincipal;
        }

        public async Task<Result<ShoppingCart>> Handle(GetUserBasketQueryRequest request, CancellationToken cancellationToken)
        {
            var userName = _claimPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).First().Value;
            var basket = await _basketDal.Get(x => x.UserName == userName).FirstOrDefaultAsync();
            if (basket == null)
                return Result.Fail(new BasketEmptyResult());

            return Result.Ok(basket);
        }
    }
}
