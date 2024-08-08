using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Commands.Response;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity.Dto;
using BestDigiSellerApp.Product.Entity.Results;
using BestDigiSellerApp.Product.Entity;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BestDigiSellerApp.Product.Application.Clients;

namespace BestDigiSellerApp.Product.Application.Product.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, Result>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FileClient _client;
        public UpdateProductCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, FileClient client)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _client = client;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var searchedProduct = await _unitOfWork.ProductDal.Get(x => x.Id == request.ProductId).FirstOrDefaultAsync();
            if (searchedProduct == null)
                return Result.Fail(new ProductNotFoundResult());

            var product = _mapper.Map(request, searchedProduct);

            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            foreach (var item in request.CategoryId)
            {
                var isCategoryExist = await _unitOfWork.CategoryDal.Get(x => x.Id == item, true).FirstOrDefaultAsync();
                if (isCategoryExist == null)
                    return Result.Fail(new CategoryNotFoundResult(item));
                product.Categories.Add(isCategoryExist);
            }
            var productPhotos = await _client.AddPhotoToFileApi(new PhotosForCreateDto { Files = request.FormFiles, Bearer = accessToken });
            if (!productPhotos.IsSuccess)
                return Result.Fail(productPhotos.Errors);

            foreach (var photo in productPhotos.Value)
            {
                product.Photos.Add(new Photo { Url = $"https://localhost:7187/Files/{photo.File}" });
            }

            var addedProduct = await _unitOfWork.ProductDal.UpdateAsync(product);
            return Result.Ok();
        }
    }
}
