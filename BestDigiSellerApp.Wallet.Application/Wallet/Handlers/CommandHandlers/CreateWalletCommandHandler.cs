using AutoMapper;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Response;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Entity;
using BestDigiSellerApp.Wallet.Entity.Enums;
using BestDigiSellerApp.Wallet.Entity.Exceptions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Handlers.CommandHandlers
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommandRequest, Result<CreateWalletCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateWalletCommandResponse>> Handle(CreateWalletCommandRequest request, CancellationToken cancellationToken)
        {
            var isWalletExist = await _unitOfWork.WalletDal.Get(x => x.UserId == request.UserEmail).Include(x => x.WalletDetails).AsNoTracking().FirstOrDefaultAsync();
            if (isWalletExist is not null)
            {
                if (isWalletExist.WalletDetails.Exists(x => x.Currency == request.Currency))
                    return Result.Fail(new AlreadyHaveCurrencyAccountResult());

                var iban = GenerateRandomIban("TR");
                isWalletExist.WalletDetails.Add(new WalletDetail { Currency = request.Currency, Iban = iban });
                await _unitOfWork.WalletDal.UpdateAsync(isWalletExist);
                return new CreateWalletCommandResponse { Iban = iban, UserEmail = request.UserEmail };
            }
            else
            {
                var wallet = _mapper.Map<BestDigiSellerApp.Wallet.Entity.Wallet>(request);
                var iban = GenerateRandomIban("TR");
                wallet.WalletDetails.Add(new WalletDetail { Currency = request.Currency, Iban = iban });
                await _unitOfWork.WalletDal.AddAsync(wallet);
                return new CreateWalletCommandResponse { Iban = iban, UserEmail = request.UserEmail };
            }
        }
        public string GenerateRandomIban(string currencyText)
        {
            string countryCode = currencyText;
            string checkDigits = "82";
            string bankCode = "00062";
            int accountNumberLength = 16;
            string accountNumber = GenerateRandomNumber(accountNumberLength);
            string iban = countryCode + checkDigits + bankCode + accountNumber;
            return iban;
        }

        private string GenerateRandomNumber(int length)
        {
            Random random = new Random();
            char[] digits = new char[length];
            for (int i = 0; i < length; i++)
            {
                digits[i] = (char)('0' + random.Next(10));
            }
            return new string(digits);
        }
    }
}
