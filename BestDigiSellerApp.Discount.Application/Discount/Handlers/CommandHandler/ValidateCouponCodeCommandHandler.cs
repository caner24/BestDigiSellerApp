using BestDigiSellerApp.Discount.Application.Discount.Commands.Request;
using BestDigiSellerApp.Discount.Data.Abstract;
using BestDigiSellerApp.Discount.Entity.Result;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Discount.Handlers.CommandHandler
{
    public class ValidateCouponCodeCommandHandler : IRequestHandler<ValidateCouponCodeCommandRequest, Result<int>>
    {
        private readonly IDiscountDal _discountDal;
        public ValidateCouponCodeCommandHandler(IDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }
        public async Task<Result<int>> Handle(ValidateCouponCodeCommandRequest request, CancellationToken cancellationToken)
        {
            var couponCode = await _discountDal.Get(x => x.CouponCode == request.CouponCode && x.DiscountUser.UserEmail == request.Email).FirstOrDefaultAsync();
            if (couponCode is null || couponCode.ExpireTime < DateTime.UtcNow)
                return Result.Fail(new CouponIsNotValid());

            return Result.Ok(couponCode.CouponPercentage);
        }
    }
}
