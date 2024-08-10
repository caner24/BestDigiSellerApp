using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDigiSellerApp.User.Application.User.Queries.Response
{
    public record GetAllUserEmailResponse
    {
        public List<string>? Email { get; init; }
    }
}
