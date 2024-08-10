using AutoMapper;
using BestDigiSellerApp.Basket.Application.Basket.Commands.Request;
using BestDigiSellerApp.Basket.Application.Clients;
using BestDigiSellerApp.Basket.Data.Abstract;
using BestDigiSellerApp.Basket.Data.Concrete;
using BestDigiSellerApp.Basket.Entity.Dto;
using BestDigiSellerApp.Basket.Entity.Result;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Basket.Application.Basket.Handlers.CommandHandlers
{
    public class CreateCheckoutSessionBasketCommandHandler : IRequestHandler<CreateCheckoutSessionBasketCommandRequest, Result<string>>
    {
        private readonly IBasketDal _basketDal;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _claimPrincipal;
        private readonly StripeClient _stripeClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateCheckoutSessionBasketCommandHandler(IBasketDal basketDal, IMapper mapper, ClaimsPrincipal claimPrincipal, StripeClient stripeClient, IHttpContextAccessor httpContextAccessor)
        {
            _basketDal = basketDal;
            _claimPrincipal = claimPrincipal;
            _stripeClient = stripeClient;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> Handle(CreateCheckoutSessionBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var user = _claimPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).First().Value;
            var userEmail = _claimPrincipal.Claims.Where(x => x.Type == ClaimTypes.Email).First().Value;
            var basket = await _basketDal.Get(x => x.UserName == user).FirstOrDefaultAsync();
            if (basket is null || basket.Items is null)
                return Result.Fail(new BasketEmptyResult());

            ShoopingCartDto shoopingCartDto = new ShoopingCartDto();
            shoopingCartDto.EmailAdress = userEmail;
            shoopingCartDto.Bearer = accessToken;
            foreach (var item in basket.Items)
            {
                shoopingCartDto.Products.Add(new ProductDto { Name = item.ProductName, Price = item.Price, ProductId = item.ProductId, Quantity = item.Quantity });
            }
            var stripeRequest = await _stripeClient.CreateCheckout(shoopingCartDto);

            if (!stripeRequest.IsSuccess)
            {
                return Result.Fail(stripeRequest.Errors);
            }

            return Result.Ok(stripeRequest.Value);
        }
    }
}
