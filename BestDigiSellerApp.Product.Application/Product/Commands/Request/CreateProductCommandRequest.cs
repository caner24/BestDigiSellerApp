using BestDigiSellerApp.Product.Application.Product.Commands.Response;
using BestDigiSellerApp.Product.Application.Validation.FluentValidation;
using BestDigiSellerApp.Product.Entity.Dto;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Commands.Request
{
    public record CreateProductCommandRequest : ProductForCreateDto, IRequest<Result<CreateProductCommandResponse>>
    {
        public List<IFormFile> FormFiles { get; init; }
    }
}
