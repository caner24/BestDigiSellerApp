using BestDigiSellerApp.Product.Application.Product.Commands.Request;
using BestDigiSellerApp.Product.Application.Product.Commands.Response;
using BestDigiSellerApp.Product.Data.Abstract;
using BestDigiSellerApp.Product.Entity.Dto;
using FluentResults;
using MediatR;
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
        public Task<Result> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
