
using BestDigiSellerApp.User.Application.User.Queries.Response;
using BestDigiSellerApp.User.Entity.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Queries.Request
{
    public record ConfirmMailQueryRequest : ConfirmMailQueryRequestDto, IRequest<ConfirmMailQueryResponse>
    {

    }
}
