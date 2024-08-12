using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Data.Abstract;
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
    public class UpdateProductQuantityChangeCommandHandler : IRequestHandler<UpdateProductQuantityChangeCommandRequest, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductQuantityChangeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateProductQuantityChangeCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductDal.Get(x => x.Id == request.ProductId,true).FirstOrDefaultAsync();
            if (product == null)
                return Result.Fail(new ProductNotFoundResult());

            var newQuantity = product.ProductDetail.Stock -= request.Quantity >= 0 ? product.ProductDetail.Stock - request.Quantity : 0;
            product.ProductDetail.Stock = newQuantity;
            await _unitOfWork.ProductDal.UpdateAsync(product);
            return Result.Ok();
        }
    }
}
