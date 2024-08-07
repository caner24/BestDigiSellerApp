using AutoMapper;
using BestDigiSellerApp.Product.Application.Clients;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Commands.Response;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity;
using BestDigiSellerApp.Product.Entity.Dto;
using BestDigiSellerApp.Product.Entity.Results;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BestDigiSellerApp.Product.Application.Product.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Result<CreateProductCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FileClient _client;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, FileClient client)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _client = client;
        }


        public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<BestDigiSellerApp.Product.Entity.Product>(request);

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

            var addedProduct = await _unitOfWork.ProductDal.AddAsync(product);
            return Result.Ok(new CreateProductCommandResponse { Id = addedProduct.Id });

            throw new NotImplementedException();
        }
    }
}
