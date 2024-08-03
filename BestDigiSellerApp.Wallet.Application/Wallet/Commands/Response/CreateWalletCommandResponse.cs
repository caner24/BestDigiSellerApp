using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.Wallet.Application.Wallet.Commands.Response
{
    public record CreateWalletCommandResponse
    {
        public string Iban { get; init; }
    }
}
