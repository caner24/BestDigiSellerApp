using BestDigiSellerApp.Invoice.Application.Invoice.Commands.Request;
using BestDigiSellerApp.Invoice.Data.Abstract;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Invoice.Application.Invoice.Handlers.CommandHandler
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommandRequest, Result>
    {
        private readonly IInvoiceDal _invoiceDal;
        public CreateInvoiceCommandHandler(IInvoiceDal invoiceDal)
        {
            _invoiceDal = invoiceDal;
        }
        public Task<Result> Handle(CreateInvoiceCommandRequest request, CancellationToken cancellationToken)
        {
            BestDigiSellerApp.Invoice.Entity.Invoice invoice = new BestDigiSellerApp.Invoice.Entity.Invoice();
            invoice.ProductId = request.ProductId;
            invoice.UserEmail = request.MailAdress;
            invoice.CashbackAmount = request.CashBackBalance;
            invoice.ProductAmount = request.Amount;
            invoice.CouponCode = request.CouponCode;
            invoice.FiecheNo = GenerateOrderCode();

            throw new NotImplementedException();
        }

        private string GenerateOrderCode() =>
       new string(Enumerable.Range(0, new Random().Next(7, 10))
                            .Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[new Random().Next(36)])
                            .ToArray());
    }
}
