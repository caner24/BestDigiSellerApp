using AutoMapper;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Request;
using BestDigiSellerApp.Wallet.Application.Wallet.Commands.Response;
using BestDigiSellerApp.Wallet.Data.Abstract;
using BestDigiSellerApp.Wallet.Entity;
using BestDigiSellerApp.Wallet.Entity.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Handlers.CommandHandlers
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommandRequest, CreateWalletCommandResponse>
    {
        private readonly IWalletDal _walletDal;
        private readonly IMapper _mapper;
        public CreateWalletCommandHandler(IWalletDal walletDal, IMapper mapper)
        {
            _mapper = mapper;
            _walletDal = walletDal;
        }
        public async Task<CreateWalletCommandResponse> Handle(CreateWalletCommandRequest request, CancellationToken cancellationToken)
        {
            var wallet = _mapper.Map<BestDigiSellerApp.Wallet.Entity.Wallet>(request);
            var iban = GenerateRandomIban("TR");
            wallet.WalletDetails.Add(new WalletDetail { Currency = Currency.TRY, Iban = iban });
            await _walletDal.AddAsync(wallet);
            return new CreateWalletCommandResponse { Iban = iban };
        }
        public string GenerateRandomIban(string currency)
        {
            string countryCode = currency;
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
