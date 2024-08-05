using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Entity.Dto
{
    public record WalletRequestDto
    {
        public string UserEmail { get; init; }
        public int Currency { get; init; }
    }
}
