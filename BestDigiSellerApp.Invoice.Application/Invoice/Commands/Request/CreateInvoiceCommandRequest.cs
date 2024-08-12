using BestDigiSellerApp.Invoice.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Application.Invoice.Commands.Request
{
    public record class CreateInvoiceCommandRequest : CreateInvoiceDto, IRequest<Result>
    {

    }
}
