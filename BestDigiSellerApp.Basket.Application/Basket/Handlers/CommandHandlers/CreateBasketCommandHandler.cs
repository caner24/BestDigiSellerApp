using AutoMapper;
using BestDigiSellerApp.Basket.Application.Basket.Commands.Request;
using BestDigiSellerApp.Basket.Application.Basket.Commands.Response;
using BestDigiSellerApp.Basket.Application.Clients;
using BestDigiSellerApp.Basket.Data.Abstract;
using BestDigiSellerApp.Basket.Entity;
using BestDigiSellerApp.Basket.Entity.Dto;
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
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommandRequest, Result<CreateBasketCommandResponse>>
    {
        private readonly IBasketDal _basketDal;
        private readonly IMapper _mapper;
        private readonly ProductClient _client;
        private readonly ClaimsPrincipal _claimPrincipal;
        public CreateBasketCommandHandler(IBasketDal basketDal, IMapper mapper, ProductClient client, ClaimsPrincipal claimPrincipal)
        {
            _basketDal = basketDal;
            _mapper = mapper;
            _client = client;
            _claimPrincipal = claimPrincipal;
        }
        public async Task<Result<CreateBasketCommandResponse>> Handle(CreateBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var userName = _claimPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).First().Value;
            var product = await _client.GetProduct(request);
            if (!product.IsSuccess)
                return Result.Fail(product.Errors);

            var userBasket = await _basketDal.Get(x => x.UserName == userName).FirstOrDefaultAsync();
            if (userBasket == null)
            {
                var mappedBasket = new BasketForCreateDto { ImageFile = product.Value.photos.First().url, MaxPoint = product.Value.productDetail.maxPoint, PointPercentage = product.Value.productDetail.pointPercentage, Price = product.Value.productDetail.price, ProductId = product.Value.id, ProductName = product.Value.name, Quantity = request.Quantity, UserName = userName };
                var mappedCart = _mapper.Map<ShoppingCart>(mappedBasket);

                var addedBasket = await _basketDal.AddAsync(mappedCart);
                return Result.Ok(new CreateBasketCommandResponse { Id = addedBasket.Id });
            }
            else
            {
                userBasket.Items.Add(new ShoppingCartItem { ImageFile = product.Value.photos.First().url, MaxPoint = product.Value.productDetail.maxPoint, PointPercentage = product.Value.productDetail.pointPercentage, Price = product.Value.productDetail.price, ProductId = product.Value.id, ProductName = product.Value.name, Quantity = request.Quantity });
                var addedBasket = await _basketDal.UpdateAsync(userBasket);
                return Result.Ok(new CreateBasketCommandResponse { Id = addedBasket.Id });
            }



        }
    }
}
