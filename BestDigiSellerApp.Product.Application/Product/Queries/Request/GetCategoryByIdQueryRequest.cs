using BestDigiSellerApp.Product.Entity;
using BestDigiSellerApp.Product.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Queries.Request
{
    public record GetCategoryByIdQueryRequest : CategoryForDeleteDto, IRequest<Result<Category>>
    {

    }
}
