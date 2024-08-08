using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Commands.Response;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity.Dto;
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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Result>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductDal.Get(x => x.Id == request.ProductId).FirstOrDefaultAsync();
            if (product == null)
                return Result.Fail(new ProductNotFoundResult());

            await _unitOfWork.ProductDal.DeleteAsync(product);
            return Result.Ok();
        }
    }
}
