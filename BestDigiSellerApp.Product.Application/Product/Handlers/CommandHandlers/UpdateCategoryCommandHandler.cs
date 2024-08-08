using AutoMapper;
using BestDigiSellerApp.Product.Application.Product.Commands.Request;
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

namespace BestDigiSellerApp.Product.Application.Product.Handlers.CommandHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryDal.Get(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (category is null)
                return Result.Fail(new CategoryNotFoundResult(request.Id));

            var mappedCategory = _mapper.Map<Category>(request);
            await _unitOfWork.CategoryDal.UpdateAsync(mappedCategory);

            return Result.Ok();

        }
    }
}
