using AutoMapper;
using BestDigiSellerApp.Discount.Application.Client;
using BestDigiSellerApp.Discount.Application.Discount.Commands.Request;
using BestDigiSellerApp.Discount.Application.Discount.Commands.Response;
using BestDigiSellerApp.Discount.Data.Abstract;
using BestDigiSellerApp.Discount.Entity;
using BestDigiSellerApp.Discount.Entity.Dto;
using FluentResults;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Discount.Application.Discount.Handlers.CommandHandler
{
    public class CreateCouponCodeCommandHandler : IRequestHandler<CreateCouponCodeCommandRequest, Result>
    {
        private readonly IUnitOfWorkDal _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserClient _userClient;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateCouponCodeCommandHandler(IUnitOfWorkDal unitOfWork, IMapper mapper, UserClient userClient, IPublishEndpoint publishEndpoint, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userClient = userClient;
            _publishEndpoint = publishEndpoint;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result> Handle(CreateCouponCodeCommandRequest request, CancellationToken cancellationToken)
        {

            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var users = await _userClient.GetUsers(new GetAllUserRequestDto { Bearer = accessToken });
            List<CreateCouponCodeCommandResponse> createCouponCodeCommandResponses = new List<CreateCouponCodeCommandResponse>();
            List<CreateCouponCodeDto> createCouponCodeDto = new List<CreateCouponCodeDto>();
            foreach (var item in users.Value)
            {
                var couponCode = GenerateCouponCode();
                var user = await _unitOfWork.DiscountUserDal.Get(x => x.UserEmail == item, true).FirstOrDefaultAsync();
                if (user == null)
                {
                    var discountUser = new DiscountUser();
                    discountUser.UserEmail = item;
                    discountUser.Discounts.Add(new Entity.Discount { CouponCode = couponCode, CouponPercentage = request.CouponPercentage, ExpireTime = request.ExpireTime });
                    var addedUser = await _unitOfWork.DiscountUserDal.AddAsync(discountUser);
                    createCouponCodeDto.Add(new CreateCouponCodeDto { ExpireTime = request.ExpireTime, CouponCode = couponCode, Email = discountUser.UserEmail, CouponPercentage = request.CouponPercentage });
                }
                else
                {
                    user.Discounts.Add(new Entity.Discount { CouponCode = couponCode, CouponPercentage = request.CouponPercentage, ExpireTime = request.ExpireTime });
                    createCouponCodeDto.Add(new CreateCouponCodeDto { ExpireTime = request.ExpireTime, CouponCode = couponCode, Email = user.UserEmail, CouponPercentage = request.CouponPercentage });
                }
            }
            await _unitOfWork.SaveChangesAsync();
            var couponCodeList = new CreateCouponCodeDtoList { CouponCodes = createCouponCodeDto };
            await _publishEndpoint.Publish(couponCodeList);
            return Result.Ok();
        }
        private string GenerateCouponCode()
        {
            int codeLength = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var couponCode = new StringBuilder(codeLength);
            for (int i = 0; i < codeLength; i++)
            {
                couponCode.Append(chars[random.Next(chars.Length)]);
            }
            return couponCode.ToString();
        }
    }
}
