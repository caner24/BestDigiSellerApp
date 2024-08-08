using BestDigiSellerApp.Product.Entity.Helpers;
using BestDigiSellerApp.Product.Entity;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Product.Application.Product.Queries.Request
{
    public class GetAllProductQueryRequest : ProductParameters, IRequest<Result<PagedList<ShapedEntity>>>
    {


    }
}
