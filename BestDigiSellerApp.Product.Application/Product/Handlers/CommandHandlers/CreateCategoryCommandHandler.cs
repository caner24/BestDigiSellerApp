using AutoMapper;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Commands.Response;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity;
using BestDigiSellerApp.Product.Entity.Results;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Handlers.CommandHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Result<CreateCategoryCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper maper)
        {
            _unitOfWork = unitOfWork;
            _mapper = maper;
        }
        public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            var addedCategory = await _unitOfWork.CategoryDal.AddAsync(category);
            if (addedCategory is null)
                return Result.Fail(new CategoryCouldNotBeAddedResult());

            return Result.Ok(new CreateCategoryCommandResponse { CategoryId = category.Id });
        }
    }
}
