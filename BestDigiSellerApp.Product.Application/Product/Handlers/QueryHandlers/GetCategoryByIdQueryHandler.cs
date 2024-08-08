using BestDigiSellerApp.Product.Application.Product.Queries.Request;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity;
using BestDigiSellerApp.Product.Entity.Results;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Handlers.QueryHandlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, Result<Category>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Category>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryDal.Get(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (category is null)
                return Result.Fail(new CategoryNotFoundResult(request.Id));

            return Result.Ok(category);
        }
    }
}
